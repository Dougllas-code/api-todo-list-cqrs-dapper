using Flunt.Notifications;
using Flunt.Validations;

namespace api_todo_list.Domain.Command;

public class UpdateTarefaCommand: Notifiable<Notification>
{
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public DateTime Updated_at { get; private set; }

    public UpdateTarefaCommand(string titulo, string descricao)
    {
        Titulo = titulo;    
        Descricao = descricao;  
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
