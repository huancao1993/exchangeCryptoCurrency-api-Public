using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Trading.Authen.Repository.Entity
{
    public partial class TradingDbAuthenContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public TradingDbAuthenContext(IConfiguration configuration)
        {
            // _configuration = configuration;
            //_context = context;
        }
        public TradingDbAuthenContext()
        {
            // _configuration = configuration;
            //_context = context;
        }
        //public TradingDbAuthenContext()
        //{
        //    // _configuration = configuration;
        //    //_context = context;
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json");

                IConfiguration Configuration = builder.Build();
                var configString = Configuration.GetConnectionString("TradingDbAuthen");
                options.UseSqlServer(configString);
            }
            //string con = _configuration.GetSection("ConnectionStrings").GetValue<string>("TradingDbAuthen");
            //options.UseSqlServer(con);
        }
        public override int SaveChanges()
        {
            BeforeSaving();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            BeforeSaving();
            return base.SaveChangesAsync();
        }
        private void BeforeSaving()
        {
            var now = DateTime.Now;
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseTable);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseTable)entityEntry.Entity).CreateAt = now;
                   // ((BaseTable)entityEntry.Entity).CreateBy = _httpContextAccessor.HttpContext?.User.Identity.Name;
                }
                if (entityEntry.State == EntityState.Modified)
                {
                    ((BaseTable)entityEntry.Entity).UpdateAt = now;
                    //((BaseTable)entityEntry.Entity).UpdateBy = _httpContextAccessor.HttpContext?.User.Identity.Name;
                }
                //if (entityEntry.State == EntityState.Deleted)
                //{
                //     entityEntry.State = EntityState.Unchanged;
                //    ((BaseTable)entityEntry.Entity).IsDeleted = true;
                //}
            };

        }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RoleAction> RoleActions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<RoleGroup> RoleGroups { get; set; }
        public virtual DbSet<RoleGroupAction> RoleGroupActions { get; set; }
        public virtual DbSet<UserHasRoleGroup> UserHasRoleGroups { get; set; }
        public virtual DbSet<Screen> Screens {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleGroup>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Users>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<RoleGroupAction>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Screen>(entity =>
            {

                entity.HasOne(d => d.Role)
                .WithMany(p => p.Screes)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasMany(p=>p.Screens)
                .WithOne()
                .HasForeignKey(d => d.ParentId)
                .IsRequired(false);
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.HasKey(e => e.IdPermission);
                    entity.HasOne(d => d.Users)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RoleAction)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.IdRoleAction);
            });
            modelBuilder.Entity<RoleGroupAction>(entity =>
            {

                entity.HasOne(d => d.RoleGroup)
                .WithMany(p => p.RoleGroupActions)
                .HasForeignKey(d => d.IdRoleGroup)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RoleAction)
                    .WithMany(p => p.RoleGroupActions)
                    .HasForeignKey(d => d.IdRoleAction)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<UserHasRoleGroup>(entity =>
            {
                entity.HasOne(d => d.User)
                .WithMany(p => p.UserHasRoleGroups)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.RoleGroup)
                    .WithMany(p => p.UserHasRoleGroups)
                    .HasForeignKey(d => d.IdRoleGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<RoleAction>(entity =>
            {

                entity.Property(e => e.Code).IsRequired().HasMaxLength(50).IsUnicode(true);
                entity.Property(e => e.RoleActionName).HasMaxLength(100);
                entity.HasOne(d => d.Role)
                      .WithMany(p => p.RoleActions)
                      .HasForeignKey(d =>d.IdRole);
            });
            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRole);
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode();
            });
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(e => e.Email).IsUnicode();
                entity.Property(e => e.Code);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
