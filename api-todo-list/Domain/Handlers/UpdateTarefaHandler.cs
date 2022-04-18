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
            return GenericCommandResult.Falha("Falha ao atualizar tarefa", command.Notifications);
        }
        #endregion

        #region VERIFICA SE TAREFA EXISTE
        var tarefa = await _tarefaRepository.FindById(id);

        if (tarefa == null)
            return GenericCommandResult.Falha("Tarefa não encontrada");
        #endregion

        #region CRIA E VALIDA TAREFA ATUALIZADA
        var tarefaAtualizada = Tarefa.Update(tarefa, command);

        tarefaAtualizada.Validate();

        if (!tarefaAtualizada.IsValid)
            return GenericCommandResult.Falha("Falha ao atualizar tarefa", tarefaAtualizada.Notifications);
        #endregion

        #region SALVA TAREFA ATUALIZADA NO BANCO
        var result = await _tarefaRepository.Update(tarefaAtualizada);

        if (result == 0)
            return GenericCommandResult.Falha("Erro ao atualizar tarefa");

        return GenericCommandResult.Sucesso("Tarefa atualizada com sucesso");
        #endregion
    }

    public async Task<GenericCommandResult> Handle(string id)
    {
        #region VALIDA ID
        if (!Guid.TryParse(id, out Guid idValido))
            return GenericCommandResult.Falha("Id inválido");
        #endregion

        #region VERIFICA SE TAREFA EXISTE
        var tarefa = await _tarefaRepository.FindById(id);

        if (tarefa == null)
            return GenericCommandResult.Falha("Tarefa não encontrada");
        #endregion

        #region CRIA E VALIDA TAREFA ATUALIZADA
        var tarefaAtualizada = Tarefa.UpdateDone(tarefa);
        tarefaAtualizada.Validate();

        if (!tarefaAtualizada.IsValid)
            return GenericCommandResult.Falha("Erro ao atualizar tarefa", tarefaAtualizada.Notifications);
        #endregion

        #region SALVA TAREFA ATUALIZADA NO BANCO
        var result = await _tarefaRepository.UpdateDone(tarefaAtualizada);

        if (result == 0)
            return GenericCommandResult.Falha("Erro ao atualizar tarefa");

        return GenericCommandResult.Sucesso("Tarefa atualizada com sucesso");
        #endregion
    }
}
