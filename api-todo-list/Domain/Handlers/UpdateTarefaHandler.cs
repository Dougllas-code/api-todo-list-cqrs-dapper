using api_todo_list.Domain.Command;
using api_todo_list.Model;
using api_todo_list.Repository;

namespace api_todo_list.Domain.Handlers;

public class UpdateTarefaHandler
{
    private readonly ITarefaRepository _tarefaRepository;

    public UpdateTarefaHandler(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public async Task<GenericCommandResult> Handle(string id, UpdateTarefaCommand command)
    {
        command.Validate();

        if (!command.IsValid)
        {
            return GenericCommandResult.Falha("Falha ao alterar tarefa", command.Notifications);
        }

        Tarefa tarefa = Tarefa.Update(id, command);

        tarefa.Validate();

        if (!tarefa.IsValid)
            return GenericCommandResult.Falha("Falha ao alterar tarefa", tarefa.Notifications);

        await _tarefaRepository.Update(tarefa);
        return GenericCommandResult.Sucesso("Tarefa alterada com sucesso");
    }
}
