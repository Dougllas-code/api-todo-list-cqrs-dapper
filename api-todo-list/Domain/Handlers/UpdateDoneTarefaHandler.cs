using api_todo_list.Domain.Command;
using api_todo_list.Repository;

namespace api_todo_list.Domain.Handlers;

public class UpdateDoneTarefaHandler
{
    private readonly ITarefaRepository _tarefaRepository;

    public UpdateDoneTarefaHandler(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    //public async Task<GenericCommandResult> Handle(string id)
    //{

    //}


}
