using Flunt.Notifications;
using Flunt.Validations;

namespace api_todo_list.Command;

public class CreateTarefaCommand: Notifiable<Notification>
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }

    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(Titulo, "Titulo", "Titulo não pode ser vazio")
            .IsNotNullOrEmpty(Descricao, "Descrição", "Descrição não pode ser vazia")
        );
    }
}
