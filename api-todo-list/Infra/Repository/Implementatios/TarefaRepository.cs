using api_todo_list.Data;
using api_todo_list.Domain.Command;
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

    public async Task<List<TarefaQueryResult>> GetAll()
    {
        using var connection = _context.Connection;

        string query = "SELECT * FROM tarefa";
        List<TarefaQueryResult> tarefas = (await connection.QueryAsync<TarefaQueryResult>(sql: query)).ToList();
        return tarefas;
    }

    public async Task Create(Tarefa tarefa)
    {
        using var connection = _context.Connection;

        string query = $"INSERT INTO tarefa(Id, Titulo, Descricao, Done, Created_at) " +
            $"VALUES('{tarefa.Id}', '{tarefa.Titulo}', '{tarefa.Descricao}', {tarefa.Done}, '{tarefa.Created_at.ToString("s")}')";

        await connection.ExecuteAsync(sql: query);
    }

    public async Task Update(Tarefa tarefa)
    {
        using var connection = _context.Connection;

        string query = @"UPDATE tarefa SET Titulo = @titulo, Descricao = @descricao, Updated_at = @updated_at WHERE Id = @id";
       
        var parametros = new DynamicParameters();
        parametros.Add("id", tarefa.Id.ToString());
        parametros.Add("titulo", tarefa.Titulo);
        parametros.Add("descricao", tarefa.Descricao);
        parametros.Add("updated_at", tarefa.Updated_at.ToString("s"));

        await connection.ExecuteAsync(sql: query, param: parametros);
    }

    /*public async Task<Tarefa> FindById(int id)
    {
        using var connection = _context.Connection;

        string query = $"SELECT * FROM tarefa WHERE Id = {id}";
        Tarefa tarefa = await connection.QueryFirstOrDefaultAsync<Tarefa>(sql: query);
        return tarefa;
    }

    public async Task<Tarefa> UpdateDone(Tarefa tarefa)
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
