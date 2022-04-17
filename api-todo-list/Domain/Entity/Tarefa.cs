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

    public Tarefa(string id, string titulo, string descricao, DateTime updated_at)
    {
        Id = Guid.Parse(id);
        Titulo = titulo;
        Descricao = descricao;
        Done = false;
        Updated_at = updated_at;
    }

    public static Tarefa Create(CreateTarefaCommand command)
    {
        return new Tarefa(command.Titulo, command.Descricao);
    }

    public static Tarefa Update(string id, UpdateTarefaCommand command)
    {
        return new Tarefa(id, command.Titulo, command.Descricao, command.Updated_at);
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



