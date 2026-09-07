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

    public Task<List<Produto>> Search(string q)
    {
        if (string.IsNullOrWhiteSpace(q))
            return GetAll();

        return _conn.Table<Produto>()
                    .Where(p => p.Descricao.Contains(q))
                    .OrderBy(p => p.Descricao)
                    .ToListAsync();
    }
}
