using MySql.Data.MySqlClient;

namespace api_todo_list.Data;

public class AppDbContext : IAsyncDisposable
{
    public MySqlConnection Connection { get; set; }

    public AppDbContext(IConfiguration configuration)
    {
        Connection = new MySqlConnection(configuration.GetConnectionString("Default"));
        Connection.Open();
    }

    public async ValueTask DisposeAsync()
    {
        await Connection.CloseAsync();
        await Connection.ClearAllPoolsAsync();
        await Connection.DisposeAsync();
    }

}
