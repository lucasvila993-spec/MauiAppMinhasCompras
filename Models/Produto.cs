using SQLite;

namespace MauiAppMinhasCompras.Models;

[Table("Produto")]
public class Produto
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public double Quantidade { get; set; }

    public double Preco { get; set; }
}
