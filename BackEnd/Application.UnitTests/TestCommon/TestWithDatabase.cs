using Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests.TestCommon;

/// <summary>
/// A test base that provides a DbContext connected to a Postgres database instance running in docker.
/// The database is dropped on dispose, recreated at construction. For this to work correctly, you need
/// to run each test sequentially, not in parallel. This test base provides the most realistic scenario for
/// Integration testing by using a real database, but doing so adds some complexity because now the test depends
/// on infrastructure.
/// </summary>
public abstract class TestWithDatabase
: IDisposable
{
    // Comment one line and leave the other to enable/disable integration tests which depend on a real Postgres database
    // running on docker.
    public const string? SKIP_INTEGRATION_TESTS = null;
    //public const string? SKIP_INTEGRATION_TESTS = "Integration tests are off.";
    
    protected AppDbContext TestDbContext { get; }

    protected TestWithDatabase()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(
                "Server=localhost;Port=5444;Database=soccer_time_test_db;User Id=user;Password=password;")
            .Options;

        try
        {
            TestDbContext = new(options);
            TestDbContext.Database.EnsureDeleted();
            TestDbContext.Database.Migrate();
        }
        catch (Npgsql.NpgsqlException e)
        {
            throw new InvalidOperationException(
                "Failed connecting to test database. For Database integration tests, a Postgres database is required to be running in Docker" +
                $"with the following specifications: Port: 5444, Database name: soccer_time_test_db, User: user, Password: password. There is a " +
                $"docker compose file in this project you can use to start this database." +
                $"You can also TURN OFF the integration tests by going to the DatabaseIntegrationTEstBase class switching the commented constants " +
                $"(at the very top) that skips tests, so this line is not commented: SKIP_INTEGRATION_TESTS = \"Integration tests are off\"");
        }



    }

    public void Dispose()
    {
        TestDbContext.Database.EnsureDeleted();
    }
}