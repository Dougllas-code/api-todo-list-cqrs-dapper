using Flunt.Notifications;

namespace api_todo_list.Domain.Command;

public class GenericCommandResult: Notification
{
    public TipoMensagem Tipo { get; private set; }
    public string Mensagem { get; private set; }
    public IReadOnlyCollection<Notification> Notificacao { get; private set; }

    private GenericCommandResult(TipoMensagem tipo, string mensagem)
    {
        Tipo = tipo;
        Mensagem = mensagem;
    }
    private GenericCommandResult(TipoMensagem tipo, string mensagem, IReadOnlyCollection<Notification> notificacao)
    {
        Tipo = tipo;
        Mensagem = mensagem;    
        Notificacao = notificacao;  
    }

    public static GenericCommandResult Sucesso(string mensagem)
    {
        return new GenericCommandResult(TipoMensagem.Sucesso, mensagem);
    }

    public static GenericCommandResult Erro(string mensagem, IReadOnlyCollection<Notification> notificacao)
    {
        return new GenericCommandResult(TipoMensagem.Erro, mensagem, notificacao);
    }

    public static GenericCommandResult Erro(string mensagem)
    {
        return new GenericCommandResult(TipoMensagem.Erro, mensagem);
    }

    public static GenericCommandResult NotFound(string mensagem)
    {
        return new GenericCommandResult(TipoMensagem.NotFound, mensagem);
    }
}

public enum TipoMensagem
{
    Sucesso,
    Erro,
    NotFound
}
