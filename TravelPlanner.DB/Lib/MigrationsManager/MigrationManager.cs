using System.Reflection;
using LinqToDB;
using TravelPlanner.Domain.Models;

namespace TravelPlanner.DB.Lib.MigrationsManager;

public class MigrationManager
{

    private readonly DbContext _dbContext = new();

    public void Init(AppConfig config)
    {
        Console.WriteLine("Running Migrations");

        _dbContext.CreateTable<MigrationsTable>(tableOptions: TableOptions.CheckExistence);
        var table = _dbContext.GetTable<MigrationsTable>();

        var migrations = typeof(MigrationManager).Assembly.GetTypes()
            .Where(t => t is { Namespace: "TravelPlanner.DB.Migrations", IsClass: true })
            .ToList();

        if (migrations.Count == 0)
        {
            Console.WriteLine("No migrations found");
            return;
        }

        migrations = migrations.OrderBy(t => long.TryParse(t.Name.Split('_')[0], out var timestamp) ? timestamp : long.MaxValue).ToList();



        foreach (var migration in migrations)
        {
            // Check if migration is already applied
            var migrationName = migration.Name;
            var forceOnDev = migration.GetCustomAttribute<ForceOnDev>() != null;
            var forceMigration = forceOnDev && config.IsDevMode();

            if (!forceMigration)
            {
                var applied = table.FirstOrDefault(m => m.ClassName == migrationName);
                if (applied != null)
                {
                    Console.WriteLine($"Migration {migrationName} already applied");
                    continue;
                }
            }

            // Apply migration
            var tempInstance = Activator.CreateInstance(migration);
            if(tempInstance == null)
            {
                Console.WriteLine($"Migration {migrationName} could not be created");
                continue;
            }

            // check if instance extens IMigration interface
            var extends = tempInstance.GetType().GetInterfaces().Any(i => i == typeof(IMigration));
            if (!extends)
            {
                Console.WriteLine($"Migration {migrationName} does not extend IMigration");
                continue;
            }
            var instance = tempInstance as IMigration;

            Console.WriteLine(forceMigration
                ? $"(Forced)Applying migration {migrationName}"
                : $"Applying migration {migrationName}");

            // Run migration
            instance?.Up(_dbContext);

            // Save migration to migrations table
            table.Insert(() => new MigrationsTable
            {
                ClassName = migrationName,
                RunAt = DateTime.Now
            });

            Console.WriteLine($"Migration {migrationName} applied");
        }

    }
}