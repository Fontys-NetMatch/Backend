# Creating a new table
1. Create the new entity in *TravelPlanner.Domain.Models.Entities
2. Create the new migration
3. Add the following line to the DbManager.cs file.
   ```csharp
   public ITable<EntityClass> Entity => this.GetTable<EntityClass>();
   ```
4. Restart the application and the table will be created.