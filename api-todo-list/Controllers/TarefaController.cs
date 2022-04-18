using api_todo_list.Command;
using api_todo_list.Domain.Command;
using api_todo_list.Domain.Handlers;
using api_todo_list.Domain.Handlers.Implementation;
using api_todo_list.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api_todo_list.Controllers;

[ApiController]
[Route(template: "v1")]
public class TarefaController : ControllerBase
{
    #region GET
    [HttpGet(template: "tarefa")]
    public async Task<IActionResult> Get([FromServices] ITarefaRepository repository)
    {
        try
        {
            var tarefas = await repository.GetAll();
            return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion

    #region POST
    [HttpPost(template: "tarefa")]
    public async Task<IActionResult> Create(
        [FromServices] CreateTarefaHandler createTarefaHandler, [FromBody] CreateTarefaCommand command)
    {
        try
        {
            var result = await createTarefaHandler.Handle(command);

            if (result.Tipo == false)
                return BadRequest(result);

            return Ok(result.Mensagem);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    #endregion

    #region PUT
    [HttpPut(template: "tarefa/{id}")]
    public async Task<IActionResult> Update(
        [FromServices] UpdateTarefaHandler updateTarefaHandler, [FromBody] UpdateTarefaCommand command,
        [FromRoute] string id)
    {
        try
        {
            var result = await updateTarefaHandler.Handle(id, command);

            if (result.Tipo == false)
                return BadRequest(result);

            return Ok(result.Mensagem);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion

    #region PATCH
    [HttpPatch(template: "tarefa/{id}")]
    public async Task<IActionResult> UpdateDone(
        [FromServices] UpdateTarefaHandler updateTarefaHandler,
        [FromRoute] string id)
    {
        try
        {
            var result = await updateTarefaHandler.Handle(id);

            if (result.Tipo == false)
                return BadRequest(result.Mensagem);

            return Ok(result.Mensagem);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion

    #region DELETE
    [HttpDelete(template: "tarefa/{id}")]
    public async Task<IActionResult> Delete(
        [FromServices] DeleteTarefaHandler deleteTarefaHandler,
        [FromRoute] string id)
    {
        try
        {
            var result = await deleteTarefaHandler.Handle(id);

            if (result.Tipo == false)
                return BadRequest(result.Mensagem);

            return Ok(result.Mensagem);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion

    #region GET BY STATUS
    [HttpGet(template: "tarefa/{status}")]
    public async Task<IActionResult> GetBYStatus(
        [FromServices] ITarefaRepository tarefaRepository,
        [FromRoute] Boolean status)
    {
        try
        {
            var tarefas = await tarefaRepository.GetByStatus(status);
            return Ok(tarefas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion
}
