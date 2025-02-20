using TravelPlanner.Domain.Interfaces;

namespace TravelPlanner.Domain.Models;

public class DbConfig
{

    public string? Host { get; set; }
    public int Port { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Database { get; set; }

    // ReSharper disable once InconsistentNaming
    public bool SSL { get; set; }
    public string ConnectionString => GetConnectionString();

    private string GetConnectionString()
    {
        if (Host == "local") throw new Exception("Please set the host in the appsettings file");

        return string.Format(
            "Server={0};Database={1};Uid={2};Pwd={3};SslMode={4}",
            Host,
            Database,
            Username,
            Password,
            SSL ? "Require" : "None"
        );
    }

}