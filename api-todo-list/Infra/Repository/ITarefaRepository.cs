using api_todo_list.Domain.Query;
using api_todo_list.Model;

namespace api_todo_list.Repository;

public interface ITarefaRepository
{
    public Task<List<TarefaQueryResult>> GetAll();

    public Task Create(Tarefa tarefa);

    public Task<TarefaQueryResult> FindById(string id);

    public Task<int> Update(Tarefa tarefa);

    public Task<int> UpdateDone(Tarefa tarefa);

    public Task<int> Delete(Guid id);

    public Task<List<TarefaQueryResult>> GetByStatus(Boolean status);
}
