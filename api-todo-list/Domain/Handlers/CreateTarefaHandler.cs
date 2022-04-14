using api_todo_list.Command;
using api_todo_list.Domain.Command;
using api_todo_list.Model;
using api_todo_list.Repository;

namespace api_todo_list.Domain.Handlers.Implementation;

public class CreateTarefaHandler
{
    private readonly ITarefaRepository _tarefaRepository;

    public CreateTarefaHandler(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public async Task<GenericCommandResult> Handle(CreateTarefaCommand command)
    {
        command.Validate();

        if (!command.IsValid)
        {
            return GenericCommandResult.Falha("Falha ao criar tarefa", command.Notifications);
        }

        Tarefa tarefa = Tarefa.Create(command);

        tarefa.Validate();

        if (!tarefa.IsValid)
            return GenericCommandResult.Falha("Falha ao criar tarefa", tarefa.Notifications);

        await _tarefaRepository.Create(tarefa);
        return GenericCommandResult.Sucesso("Tarefa criada com sucesso");
    }
}
