using MySqlConnector;

namespace GiftCardCommerce.PeriwinkleWhiskers.Services;

public class DatabaseAccessFactory
{
    private readonly string _connectionString;

    public DatabaseAccessFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public MySqlConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}