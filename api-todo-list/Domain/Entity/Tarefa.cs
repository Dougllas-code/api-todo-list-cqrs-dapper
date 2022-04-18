using api_todo_list.Command;
using api_todo_list.Domain.Command;
using api_todo_list.Domain.Query;
using Flunt.Notifications;
using Flunt.Validations;

namespace api_todo_list.Model;

public class Tarefa : Notifiable<Notification>
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Boolean Done { get; set; }
    public DateTime Created_at { get; set; }
    public DateTime Updated_at { get; set; }

    public Tarefa(string titulo, string descricao)
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Descricao = descricao;
        Done = false;
        Created_at = DateTime.Now;
    }

    public Tarefa(TarefaQueryResult tarefa, UpdateTarefaCommand command)
    {
        Id = Guid.Parse(tarefa.Id);
        Titulo = command.Titulo;
        Descricao = command.Descricao;
        Done = tarefa.Done;
        Created_at = tarefa.Created_at;
        Updated_at = command.Updated_at;
    }

    public Tarefa(TarefaQueryResult tarefa)
    {
        Id = Guid.Parse(tarefa.Id);
        Titulo = tarefa.Titulo;
        Descricao = tarefa.Descricao;
        Done = true;
        Created_at = tarefa.Created_at;
        Updated_at = DateTime.Now;
    }

    public static Tarefa Create(CreateTarefaCommand command)
    {
        return new Tarefa(command.Titulo, command.Descricao);
    }

    public static Tarefa Update(TarefaQueryResult tarefa, UpdateTarefaCommand command)
    {
        return new Tarefa(tarefa, command);
    }

    public static Tarefa UpdateDone(TarefaQueryResult tarefa)
    {
        return new Tarefa(tarefa);
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



