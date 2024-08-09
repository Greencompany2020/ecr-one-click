using SQLite;

namespace EcrOneClick.Infrastructure.Database.Configurations;

public static class SqliteConfiguration
{
    private const string DBFileName = "EcrOneClick.db3";

    public const SQLiteOpenFlags Flags = SQLiteOpenFlags.Create 
                                         | SQLiteOpenFlags.ReadWrite
                                         | SQLiteOpenFlags.SharedCache;

    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DBFileName);
}