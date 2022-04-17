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
        var tarefas = await repository.GetAll();
        return Ok(tarefas);
    }
    #endregion

    #region POST
    [HttpPost(template: "tarefa")]
    public async Task<IActionResult> Create(
        [FromServices] CreateTarefaHandler createTarefaHandler, [FromBody] CreateTarefaCommand command)
    {
        try
        {
            GenericCommandResult result = await createTarefaHandler.Handle(command);

            if(result.Tipo == false)
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
            GenericCommandResult result = await updateTarefaHandler.Handle(id, command);

            if (result.Tipo == false)
                return BadRequest(result);

            return Ok(result.Mensagem);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
    #endregion

    /*[HttpPatch(template: "tarefa/{id}")]
    public async Task<IActionResult> UpdateDone(
        [FromServices] ITarefaRepository context,
        [FromRoute] int id)
    {
        var tarefa = await _tarefaReposity.FindById(context, id);

        if (tarefa == null)
            return NotFound();

        try
        {
            var tarefaAtualizada = await _tarefaReposity.UpdateDone(context, tarefa);
            return Ok(tarefaAtualizada);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete(template: "tarefa/{id}")]
    public async Task<IActionResult> Delete([FromServices] AppDbContext context, [FromRoute] int id)
    {
        var tarefa = await _tarefaReposity.FindById(context, id);

        if (tarefa == null)
            return NotFound();

        try
        {
            await _tarefaReposity.Delete(context, tarefa);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpGet(template: "tarefa/{status}")]
    public async Task<IActionResult> GetBYStatus([FromRoute] Boolean status)
    {
        var tarefas = await _tarefaReposity.GetByStatus(status);
        return Ok(tarefas);
    }*/
}
