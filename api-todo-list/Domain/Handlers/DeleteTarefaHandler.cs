using api_todo_list.Domain.Command;
using api_todo_list.Repository;

namespace api_todo_list.Domain.Handlers;

public class DeleteTarefaHandler
{
    private readonly ITarefaRepository _tarefaRepository;

    public DeleteTarefaHandler(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public async Task<GenericCommandResult> Handle(Guid id)
    {
        #region VERIFICA SE TAREFA EXISTE
        var tarefa = await _tarefaRepository.FindById(id);

        if (tarefa == null)
            return GenericCommandResult.NotFound("Tarefa não encontrada");
        #endregion

        #region EXCLUI TAREFA NO BANCO DE DADOS
        var result = await _tarefaRepository.Delete(id);

        if (result == 0)
            return GenericCommandResult.Erro("Erro ao excluir tarefa");

        return GenericCommandResult.Sucesso("Tarefa excluída com sucesso");
        #endregion
    }

}
