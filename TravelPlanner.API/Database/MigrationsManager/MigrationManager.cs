using LinqToDB;

namespace TravelPlanner.API.Database.MigrationsManager;

public class MigrationManager
{

    private readonly DbContext _dbContext = new();

    public void Init()
    {
        Console.WriteLine("Running Migrations");

        _dbContext.CreateTable<MigrationsTable>(tableOptions: TableOptions.CheckExistence);
        var table = _dbContext.GetTable<MigrationsTable>();

        var migrations = typeof(MigrationManager).Assembly.GetTypes()
            .Where(t => t is { Namespace: "TravelPlanner.API.Database.Migrations", IsClass: true });
        // order by desc first part of name split on _
        migrations = migrations.OrderByDescending(t => t.Name.Split('_')[0]);

        foreach (var migration in migrations)
        {
            // Check if migration is already applied
            var migrationName = migration.Name;
            var applied = table.FirstOrDefault(m => m.ClassName == migrationName);
            if (applied != null)
            {
                Console.WriteLine($"Migration {migrationName} already applied");
                continue;
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

            Console.WriteLine($"Applying migration {migrationName}");

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