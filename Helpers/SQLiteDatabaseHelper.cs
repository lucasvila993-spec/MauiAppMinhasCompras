using SQLite;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Helpers;

public class SQLiteDatabaseHelper
{
    readonly SQLiteAsyncConnection _conn;

    public SQLiteDatabaseHelper(string path)
    {
        _conn = new SQLiteAsyncConnection(path);
        Task.Run(() => _conn.CreateTableAsync<Produto>()).Wait();
    }

    public Task<int> Insert(Produto p)
    {
        return _conn.InsertAsync(p);
    }

    public Task<int> Update(Produto p)
    {
        return _conn.UpdateAsync(p);
    }

    public Task<int> Delete(int id)
    {
        return _conn.Table<Produto>()
                    .Where(i => i.Id == id)
                    .DeleteAsync();
    }

    public Task<List<Produto>> GetAll()
    {
        return _conn.Table<Produto>()
                    .OrderBy(p => p.Descricao)
                    .ToListAsync();
    }

    public Task<List<Produto>> Search(string q, string? categoria = null)
    {
        var query = _conn.Table<Produto>();

        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(p => p.Descricao.Contains(q));

        if (!string.IsNullOrWhiteSpace(categoria) && categoria != Categorias.Todas)
            query = query.Where(p => p.Categoria == categoria);

        return query.OrderBy(p => p.Descricao).ToListAsync();
    }

    public async Task<List<CategoriaTotal>> GetTotalPorCategoria()
    {
        var produtos = await _conn.Table<Produto>().ToListAsync();

        return produtos
            .GroupBy(p => string.IsNullOrWhiteSpace(p.Categoria) ? "Outros" : p.Categoria)
            .Select(g => new CategoriaTotal
            {
                Categoria = g.Key,
                QuantidadeItens = g.Count(),
                Total = g.Sum(p => p.Quantidade * p.Preco),
            })
            .OrderByDescending(c => c.Total)
            .ToList();
    }
}
