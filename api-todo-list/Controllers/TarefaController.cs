using api_todo_list.Command;
using api_todo_list.Domain.Command;
using api_todo_list.Domain.Handlers;
using api_todo_list.Domain.Handlers.Implementation;
using api_todo_list.Repository;
using Microsoft.AspNetCore.Mvc;

namespace api_todo_list.Controllers;

[ApiController]
[Route("v1/tarefas")]
public class TarefaController : ControllerBase
{
    #region GET
    [HttpGet]
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
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateTarefaHandler createTarefaHandler, [FromBody] CreateTarefaCommand command)
    {
        try
        {
            var result = await createTarefaHandler.Handle(command);

            if (result.Tipo == TipoMensagem.Erro)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
    #endregion

    #region PUT
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromServices] UpdateTarefaHandler updateTarefaHandler, [FromBody] UpdateTarefaCommand command)
    {
        try
        {
            var result = await updateTarefaHandler.Handle(command);

            if (result.Tipo == TipoMensagem.NotFound)
                return NotFound(result);

            if (result.Tipo == TipoMensagem.Erro)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion

    #region PATCH
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateDone(
        [FromServices] UpdateTarefaHandler updateTarefaHandler,
        [FromRoute] Guid id)
    {
        try
        {
            var result = await updateTarefaHandler.Handle(id);

            if (result.Tipo == TipoMensagem.NotFound)
                return NotFound(result);

            if (result.Tipo == TipoMensagem.Erro)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion

    #region DELETE
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromServices] DeleteTarefaHandler deleteTarefaHandler,
        [FromRoute] Guid id)
    {
        try
        {
            var result = await deleteTarefaHandler.Handle(id);

            if(result.Tipo == TipoMensagem.NotFound)
                return NotFound(result);
    
            if (result.Tipo == TipoMensagem.Erro)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion

    #region GET BY STATUS
    [HttpGet("{status}")]
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
