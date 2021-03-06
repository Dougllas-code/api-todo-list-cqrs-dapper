using api_todo_list.Data;
using api_todo_list.Domain.Query;
using api_todo_list.Model;
using Dapper;

namespace api_todo_list.Repository.Implementatios;

public class TarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;

    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }

    #region BUSCAR TODAS AS TAREFAS
    public async Task<List<TarefaQueryResult>> GetAll()
    {
        string query = "SELECT * FROM tarefa";

        using (var connection = _context.Connection)
        {
            List<TarefaQueryResult> tarefas = (await connection.QueryAsync<TarefaQueryResult>(sql: query)).ToList();
            return tarefas;
        }
    }
    #endregion

    #region CRIAR TAREFA
    public async Task Create(Tarefa tarefa)
    {
        string query = @"INSERT INTO tarefa(Id, Titulo, Descricao, Done, Created_at) 
                         VALUES(@id, @titulo, @descricao, @done, @created_at)";

        var parametros = new DynamicParameters();
        parametros.Add("id", tarefa.Id.ToString());
        parametros.Add("titulo", tarefa.Titulo);
        parametros.Add("descricao", tarefa.Descricao);
        parametros.Add("done", tarefa.Done);
        parametros.Add("created_at", tarefa.Created_at.ToString("s"));

        using (var connection = _context.Connection)
        {
            await connection.ExecuteAsync(sql: query, param: parametros);
        }
    }
    #endregion

    #region BUSCAR TAREFA PELO ID
    public async Task<TarefaQueryResult> FindById(Guid id)
    {
        string query = @"SELECT * FROM tarefa WHERE Id = @id";

        var parametros = new DynamicParameters();
        parametros.Add("id", id.ToString());

        using (var connection = _context.Connection)
        {
            TarefaQueryResult tarefa = await connection
                .QueryFirstOrDefaultAsync<TarefaQueryResult>(sql: query, param: parametros);

            return tarefa;
        }
    }
    #endregion

    #region ALTERAR DADOS DA TAREFA
    public async Task<int> Update(Tarefa tarefa)
    {
        string query = @"UPDATE tarefa 
                         SET Titulo = @titulo, Descricao = @descricao, Updated_at = @updated_at 
                         WHERE Id = @id";

        var parametros = new DynamicParameters();
        parametros.Add("id", tarefa.Id.ToString());
        parametros.Add("titulo", tarefa.Titulo);
        parametros.Add("descricao", tarefa.Descricao);
        parametros.Add("updated_at", tarefa.Updated_at.ToString("s")); // yyyy-MM-dd

        using (var connection = _context.Connection)
        {
            var result = await connection.ExecuteAsync(sql: query, param: parametros);
            return result;
        }
    }
    #endregion

    #region ALTERA STATUS DA TAREFA PARA CONCLUÍDA
    public async Task<int> UpdateDone(Tarefa tarefa)
    {
        string query = @"UPDATE tarefa 
                         SET Done = @done, Updated_at = @updated_at 
                         WHERE Id = @id";

        var parametros = new DynamicParameters();
        parametros.Add("done", tarefa.Done);
        parametros.Add("updated_at", tarefa.Updated_at.ToString("s")); // yyyy-MM-dd 
        parametros.Add("id", tarefa.Id.ToString());

        using (var connection = _context.Connection)
        {
            var result = await connection.ExecuteAsync(sql: query, param: parametros);
            return result;
        }
    }
    #endregion

    #region EXCLUIR TAREFA
    public async Task<int> Delete(Guid id)
    {
        string query = @"DELETE FROM tarefa WHERE Id = @id";

        var parametros = new DynamicParameters();
        parametros.Add("id", id.ToString());

        using (var connection = _context.Connection)
        {
            var result = await connection.ExecuteAsync(sql: query, param: parametros);
            return result;
        }
    }
    #endregion

    #region BUSCAR TAREFAS POR STATUS
    public async Task<List<TarefaQueryResult>> GetByStatus(Boolean status)
    {
        string query = $"SELECT * FROM tarefa WHERE Done = @status";

        var parametros = new DynamicParameters();
        parametros.Add("status", status);

        using (var connection = _context.Connection)
        {
            List<TarefaQueryResult> tarefas = (await connection
                .QueryAsync<TarefaQueryResult>(sql: query, param: parametros))
                .ToList();
            return tarefas;
        }
    }
    #endregion
}
