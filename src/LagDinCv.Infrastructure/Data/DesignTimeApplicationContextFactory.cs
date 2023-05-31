namespace LagDinCv.Infrastructure.Data;

using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

// ReSharper disable once UnusedType.Global
/// <summary>
/// This class is only used by `dotnet ef`-CLI tool when running migrations
/// </summary>
public class DesignTimeApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = GetConnectionString();
        var version = ServerVersion.AutoDetect(connectionString);
        optionsBuilder.UseMySql(connectionString, version);

        return new ApplicationDbContext(optionsBuilder.Options);
    }

    private static string GetConnectionString()
    {
        var filePath = $"{Directory.GetCurrentDirectory()}../../LagDinCv.WebUI/appsettings.Development.json";
        var fileContent = File.ReadAllText(filePath);
        var connectionStrings = JsonSerializer.Deserialize<AppSettings>(fileContent)
                                ?? throw new NullReferenceException("connectionString can't be null");

        return connectionStrings.ConnectionStrings.DefaultConnection;
    }
}

internal class AppSettings
{
    public required ConnectionStrings ConnectionStrings { get; set; }
}

internal class ConnectionStrings
{
    public string DefaultConnection { get; set; } = string.Empty;
}