using Flunt.Notifications;
using Flunt.Validations;

namespace api_todo_list.Domain.Command;

public class UpdateTarefaCommand: Notifiable<Notification>
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime Updated_at { get; private set; }

    public UpdateTarefaCommand()
    {
        Updated_at = DateTime.Now;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
            .Requires()
            .IsNotNull(Updated_at, "Updated_at", "Data de atualização vazia"));
    }
}
