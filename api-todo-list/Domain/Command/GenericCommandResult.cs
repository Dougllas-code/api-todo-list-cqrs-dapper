using Flunt.Notifications;

namespace api_todo_list.Domain.Command;

public class GenericCommandResult: Notification
{
    public Boolean Tipo { get; private set; }
    public string Mensagem { get; private set; }
    public IReadOnlyCollection<Notification> Notificacao { get; private set; }

    private GenericCommandResult(Boolean tipo, string mensagem)
    {
        Tipo = tipo;
        Mensagem = mensagem;
    }
    private GenericCommandResult(Boolean tipo, string mensagem, IReadOnlyCollection<Notification> notificacao)
    {
        Tipo = tipo;
        Mensagem = mensagem;    
        Notificacao = notificacao;  
    }

    public static GenericCommandResult Sucesso(string mensagem)
    {
        return new GenericCommandResult(true, mensagem);
    }

    public static GenericCommandResult Falha(string mensagem, IReadOnlyCollection<Notification> notificacao)
    {
        return new GenericCommandResult(false, mensagem, notificacao);
    }

    public static GenericCommandResult Falha(string mensagem)
    {
        return new GenericCommandResult(false, mensagem);
    }
}
