dotnet ef dbcontext scaffold "Server=HUANCAO;Database=dbAuthen;User Id=sa;Password=123456;" Microsoft.EntityFrameworkCore.SqlServer -o Entities -c DataContext --no-onconfiguring
dotnet ef migrations add updateDatabase --context TradingDbAuthenContext --output-dir Migrations
dotnet ef migrations add addScree --context DataContext --output-dir Migrations
dotnet ef migrations add updateRoles --context DataContext --output-dir Migrations
dotnet ef migrations add initialCreate --context DataContext --output-dir Migrations
dotnet ef migrations add dumpData --context DataContext --output-dir Migrations
dotnet ef migrations add updateUsers --context DataContext --output-dir Migrations
dotnet ef migrations add updateDeparment --context DataContext --output-dir Migrations

dotnet ef migrations add dumpdataRoleAction --context DataContext --output-dir Migrations
dotnet ef migrations add dumpdataRoleAction10032022 --context DataContext --output-dir Migrations

dotnet ef migrations add dumpdataRoleAction24032022 --context DataContext --output-dir Migrations
dotnet ef migrations add dumpdataRoleAction07042022 --context DataContext --output-dir Migrations
dotnet ef migrations add dumpdataRoleAction27042022 --context DataContext --output-dir Migrations
