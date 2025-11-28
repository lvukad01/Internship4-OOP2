using Npgsql;
using System.Data;

public class DapperConnectionFactory
{
    private readonly string _connectionString;

    public DapperConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
