using api_todo_list.Command;
using api_todo_list.Domain.Command;
using api_todo_list.Domain.Query;
using Flunt.Notifications;
using Flunt.Validations;

namespace api_todo_list.Model;

public class Tarefa : Notifiable<Notification>
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public Boolean Done { get; private set; }
    public DateTime Created_at { get; private set; }
    public DateTime Updated_at { get; private set; }

    public static Tarefa Create(CreateTarefaCommand command)
    {
        var tarefa = new Tarefa
        {
            Id = Guid.NewGuid(),
            Titulo = command.Titulo,
            Descricao = command.Descricao,
            Done = false,
            Created_at = DateTime.Now
        };

        tarefa.Validate();

        return tarefa;
    }

    public static Tarefa Update(TarefaQueryResult queryTarefa, UpdateTarefaCommand command)
    {
        var tarefa = new Tarefa
        {
            Id = Guid.Parse(queryTarefa.Id),
            Titulo = command.Titulo,
            Descricao = command.Descricao,
            Done = false,
            Created_at = queryTarefa.Created_at,
            Updated_at = DateTime.Now
        };

        tarefa.Validate();

        return tarefa;
    }

    public static Tarefa UpdateDone(TarefaQueryResult queryTarefa)
    {
        var tarefa = new Tarefa
        {
            Id = Guid.Parse(queryTarefa.Id),
            Titulo = queryTarefa.Titulo,
            Descricao = queryTarefa.Descricao,
            Done = true,
            Created_at = queryTarefa.Created_at,
            Updated_at = DateTime.Now
        };

        tarefa.Validate();

        return tarefa;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(Id, "Id não pode ser vazio")
                .IsLowerOrEqualsThan(Titulo, 60, "Titulo", "O Titulo deve conter até 60 caracteres")
                .IsLowerOrEqualsThan(Descricao, 150, "Descrição", " A Descrição deve conter até 150 caracteres"));
    }
}



