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
        #region VALIDA O COMMAND
        command.Validate();

        if (!command.IsValid)
        {
            return GenericCommandResult.Erro("Erro ao criar tarefa", command.Notifications);
        }
        #endregion

        #region CRIA E VALIDA A TAREFA
        var tarefa = Tarefa.Create(command);

        if (!tarefa.IsValid)
            return GenericCommandResult.Erro("Falha ao criar tarefa", tarefa.Notifications);
        #endregion

        #region INSERE A TAREFA NO BANCO
        await _tarefaRepository.Create(tarefa);
        return GenericCommandResult.Sucesso("Tarefa criada com sucesso");
        #endregion
    }
}
