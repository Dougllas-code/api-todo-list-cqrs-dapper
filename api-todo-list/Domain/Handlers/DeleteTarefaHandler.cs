using api_todo_list.Domain.Command;
using api_todo_list.Repository;

namespace api_todo_list.Domain.Handlers;

public class DeleteTarefaHandler
{
    private readonly ITarefaRepository _repository;

    public DeleteTarefaHandler(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<GenericCommandResult> Handle(string id)
    {
        #region VALIDA ID
        if (!Guid.TryParse(id, out Guid idValido))
            return GenericCommandResult.Falha("Id inválido");
        #endregion

        #region EXCLUI TAREFA NO BANCO DE DADOS
        var result = await _repository.Delete(idValido);

        if (result == 0)
            return GenericCommandResult.Falha("Erro ao deletar tarefa");

        return GenericCommandResult.Sucesso("Tarefa excluída com sucesso");
        #endregion
    }

}
