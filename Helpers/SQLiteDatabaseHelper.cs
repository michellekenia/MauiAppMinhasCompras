using SQLite;
using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        private readonly SQLiteAsyncConnection _db;

        public SQLiteDatabaseHelper(string path)
        {
            _db = new SQLiteAsyncConnection(path);
            _ = _db.CreateTableAsync<Produto>();
        }

        public Task<List<Produto>> ListarAsync()
            => _db.Table<Produto>().OrderBy(p => p.Descricao).ToListAsync();

        public Task<Produto?> BuscarPorIdAsync(int id)
            => _db.FindAsync<Produto>(id);

        public Task<int> InserirAsync(Produto p)
            => _db.InsertAsync(p);

        public Task<int> AtualizarAsync(Produto p)
            => _db.UpdateAsync(p);

        public Task<int> RemoverAsync(Produto p)
            => _db.DeleteAsync(p);
    }
}