using api_todo_list.Domain.Command;
using api_todo_list.Domain.Query;
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
        #region VALIDA COMMAND
        command.Validate();

        if (!command.IsValid)
        {
            return GenericCommandResult.Falha("Falha ao alterar tarefa", command.Notifications);
        }
        #endregion

        //#region VERIFICA SE TAREFA EXISTE
        //TarefaQueryResult tarefaExiste = await _tarefaRepository.FindById(id);

        //if (tarefaExiste == null)
        //    return GenericCommandResult.Falha("Tarefa não existe");
        //#endregion

        #region CRIA E VALIDA TAREFA ATUALIZADA
        Tarefa tarefaAtualizada = Tarefa.Update(id, command);

        tarefaAtualizada.Validate();

        if (!tarefaAtualizada.IsValid)
            return GenericCommandResult.Falha("Falha ao alterar tarefa", tarefaAtualizada.Notifications);
        #endregion

        #region SALVA TAREFA ATUALIZADA NO BANCO
        await _tarefaRepository.Update(tarefaAtualizada);
        return GenericCommandResult.Sucesso("Tarefa alterada com sucesso");
        #endregion
    }
}
