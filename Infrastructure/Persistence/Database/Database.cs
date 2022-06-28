using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Database: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder
            .UseSqlite(SqliteInMemoryDatabase.Connection);
}

public static class SqliteInMemoryDatabase
{
    private static object key = new();
    static SqliteInMemoryDatabase()
    {
        lock (key)
        {
            Connection = new SqliteConnection("Filename=:memory:");
            // We want to keep the connection open
            // and reuse it since our database is in-memory
            // Warning: don't use this with multiple clients
            // and closing the connection destroy the database
            Connection.Open();
        }
    }
        
    public static SqliteConnection Connection { get; }
}