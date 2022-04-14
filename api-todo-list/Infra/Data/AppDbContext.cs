using MySqlConnector;
using System.Data;

namespace api_todo_list.Data;

public class AppDbContext : IDisposable
{
    public IDbConnection Connection { get; set; }

    public AppDbContext(IConfiguration configuration)
    {
        Connection = new MySqlConnection(configuration.GetConnectionString("mysqldb"));
        Connection.Open();
    }
    public void Dispose() => Connection?.Dispose();

}
