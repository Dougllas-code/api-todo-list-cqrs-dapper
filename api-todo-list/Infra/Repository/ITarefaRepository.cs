using api_todo_list.Domain.Command;
using api_todo_list.Domain.Query;
using api_todo_list.Model;

namespace api_todo_list.Repository;

public interface ITarefaRepository
{
    public Task<List<TarefaQueryResult>> GetAll();

    public Task Create(Tarefa tarefa);

    public Task Update(Tarefa tarefa);

    /*public Task<Tarefa> FindById(int id);

    public Task UpdateDone(Tarefa tarefa);

    public Task Delete(Tarefa tarefa);

    public Task<List<Tarefa>> GetByStatus(Boolean status);*/
}
