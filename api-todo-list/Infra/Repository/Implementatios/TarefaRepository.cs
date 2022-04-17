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
        using (var connection = _context.Connection)
        {
            string query = "SELECT * FROM tarefa";
            List<TarefaQueryResult> tarefas = (await connection.QueryAsync<TarefaQueryResult>(sql: query)).ToList();
            return tarefas;
        }
    }
    #endregion

    #region CRIAR TAREFA
    public async Task Create(Tarefa tarefa)
    {
        using (var connection = _context.Connection)
        {
            var parametros = new DynamicParameters();
            parametros.Add("id", tarefa.Id.ToString());
            parametros.Add("titulo", tarefa.Titulo);
            parametros.Add("descricao", tarefa.Descricao);
            parametros.Add("done", tarefa.Done);
            parametros.Add("created_at", tarefa.Created_at.ToString("s"));

            string query = @"INSERT INTO tarefa(Id, Titulo, Descricao, Done, Created_at) VALUES(@id, @titulo, @descricao, @done, @created_at)";

            await connection.ExecuteAsync(sql: query, param: parametros);
        }
    }
    #endregion

    #region BUSCAR TAREFA PELO ID
    public async Task<TarefaQueryResult> FindById(string idTarefa)
    {
        using (var connection = _context.Connection)
        {
            string query = @"SELECT * FROM tarefa WHERE Id = @id";

            var parametros = new DynamicParameters();
            parametros.Add("id", idTarefa);

            TarefaQueryResult tarefa = await connection
                .QueryFirstOrDefaultAsync<TarefaQueryResult>(sql: query, param: parametros);

            return tarefa;
        }
    }
    #endregion

    #region ALTERAR DADOS DA TAREFA
    public async Task Update(Tarefa tarefa)
    {
        using (var connection = _context.Connection)
        {
            string query = @"UPDATE tarefa SET Titulo = @titulo, Descricao = @descricao, Updated_at = @updated_at WHERE Id = @id";

            var parametros = new DynamicParameters();
            parametros.Add("id", tarefa.Id.ToString());
            parametros.Add("titulo", tarefa.Titulo);
            parametros.Add("descricao", tarefa.Descricao);
            parametros.Add("updated_at", tarefa.Updated_at.ToString("s")); // yyyy-MM-dd

            await connection.ExecuteAsync(sql: query, param: parametros);
        }

    }
    #endregion

    /*public async Task<Tarefa> UpdateDone(Tarefa tarefa)
    {
        tarefa.Done = true;

        context.Tarefa.Update(tarefa);
        await context.SaveChangesAsync();

        return tarefa;
    }

    public async Task Delete(Tarefa tarefa)
    {
        context.Tarefa.Remove(tarefa);
        await context.SaveChangesAsync();
    }

    public async Task<List<Tarefa>> GetByStatus(Boolean status)
    {
        using var connection = _context.Connection; 

        string query = $"SELECT Id AS id, Titulo AS titulo, Descricao AS descricao, Done AS done FROM tarefa WHERE Done = {status}";
        List<Tarefa> tarefas = (await connection.QueryAsync<Tarefa>(sql: query)).ToList();
        return tarefas;
    }*/
}
