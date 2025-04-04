using TravelPlanner.DB.Lib;

namespace TravelPlanner.DB;

public class DbUtils
{

    public static void GenerateForeignKey(
        DbContext dbContext,
        string sourceTableName,
        string targetTableName,
        string targetTableColName
    ) {
        var constraintName = $"FK_{sourceTableName}_{targetTableName}";

        // Remove trailing 's' from targetTableName if present
        var targetTableResourceName = targetTableName.EndsWith("s") ? targetTableName[..^1] : targetTableName;

        using var checkCmd = dbContext.CreateCommand();
        checkCmd.CommandText = $@"
        SELECT COUNT(*)
        FROM information_schema.TABLE_CONSTRAINTS
        WHERE CONSTRAINT_NAME = '{constraintName}'
        AND TABLE_NAME = '{sourceTableName}'";

        var exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;

        if (exists) return;

        using var alterCmd = dbContext.CreateCommand();
        alterCmd.CommandText = $@"
            ALTER TABLE {sourceTableName}
            ADD CONSTRAINT {constraintName}
            FOREIGN KEY ({targetTableResourceName}_{targetTableColName}) 
            REFERENCES {targetTableName}({targetTableColName})";

        alterCmd.ExecuteNonQuery();
    }

    public static void GenerateUniqueConstraint(
        DbContext dbContext,
        string tableName,
        string colName
    ){
        var constraintName = $"UC_{tableName}_{colName}";

        using var checkCmd = dbContext.CreateCommand();
        checkCmd.CommandText = $@"
        SELECT COUNT(*)
        FROM information_schema.TABLE_CONSTRAINTS
        WHERE CONSTRAINT_NAME = '{constraintName}'
        AND TABLE_NAME = '{tableName}'";

        var exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;

        if (exists) return;

        using var alterCmd = dbContext.CreateCommand();
        alterCmd.CommandText = $@"
            ALTER TABLE {tableName}
            ADD CONSTRAINT {constraintName}
            UNIQUE ({colName})";

        alterCmd.ExecuteNonQuery();
    }

}