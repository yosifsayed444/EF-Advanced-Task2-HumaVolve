
ASP.NET Core Web API (.NET 10) + EF Core 10 + SQL Server.


```bash
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef migrations add Init
dotnet ef database update
dotnet run
```
