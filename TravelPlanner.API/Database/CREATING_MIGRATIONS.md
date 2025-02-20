# Create a new migration
1. Create a new class in the Migrations folder.
   Filename mist start with the current [unix timestamp](https://www.unixtimestamp.com/) then a name. 
   **e.g. 1612345678_CreateTable.cs**
2. In the class extend the IMigration interface.
3. Implement the Up method.
4. Restart the application to apply the migration.