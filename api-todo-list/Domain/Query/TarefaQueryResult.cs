namespace api_todo_list.Domain.Query;

public class TarefaQueryResult
{
    public string Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Boolean Done { get; set; }
    public DateTime Created_at { get; set; }
    public DateTime Updated_at { get; set; }
}
