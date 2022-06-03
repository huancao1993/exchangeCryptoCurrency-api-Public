using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Trading.Authen.Api.Helpers;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.OpenApi.Models;
using Trading.Authen.Api.Interfaces;
using EventBus.RabbitMQ.Standard.Options;
using EventBus.RabbitMQ.Standard.Configuration;
using Trading.Authen.Api.Extention;
using EventBus.Base.Standard.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Trading.Authen.Api.Commons;
using System.IO;
using System.Reflection;
using EventBusiness.Interfaces;
using EventBusiness.Services;
using Trading.Repository.UnitOfWork;
using Trading.Repository.Repositories.Generics;
using Trading.Services.Implementations;
using Trading.Repository.Entity;

namespace Trading.Authen.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<TradingDbAuthenContext>();
            services.AddCors();
            services.AddControllers(options =>
            {
                options.Filters.Add(new ValidateModelStateFilter());
                options.Filters.Add(new HttpResponseExceptionFilter());
            });
            
            //services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // configure strongly typed settings objects
            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer( x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated =  context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        //var userId = int.Parse(context.Principal.Identity.Name);
                        var user =  userService.GetByName(context.Principal.Identity.Name).Result;
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return  Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationEventService,NotificationEventService>();
            
            // services.AddScoped<IEmployeesService, EmployeesService>();

            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            { jwtSecurityScheme, Array.Empty<string>() }
                        });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setup.IncludeXmlComments(xmlPath);
            });


            // rabbit mq 
            var rabbitMqOptions = _configuration.GetSection("RabbitMq").Get<RabbitMqOptions>();
            services.AddRabbitMqConnection(rabbitMqOptions);
            services.AddRabbitMqRegistration(rabbitMqOptions);
            services.AddEventBusiness();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TradingDbAuthenContext dataContext)
        {
            // migrate any database changes on startup (includes initial db creation)
            dataContext.Database.Migrate();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            // Subscribe event rabbit
             app.SubscribeToEvents();
        }
    }
}
