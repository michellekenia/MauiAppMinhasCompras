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
        {
            try
            {
                return _db.Table<Produto>().OrderBy(p => p.Descricao).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar produtos: {ex.Message}");
                return Task.FromResult(new List<Produto>());
            }
        }

        public Task<Produto?> BuscarPorIdAsync(int id)
        {
            try
            {
                return _db.FindAsync<Produto>(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar produto por ID: {ex.Message}");
                return Task.FromResult<Produto?>(null);
            }
        }

        public Task<int> InserirAsync(Produto p)
        {
            try
            {
                return _db.InsertAsync(p);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir produto: {ex.Message}");
                return Task.FromResult(0);
            }
        }

        public Task<int> AtualizarAsync(Produto p)
        {
            try
            {
                return _db.UpdateAsync(p);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return Task.FromResult(0);
            }
        }

        public Task<int> RemoverAsync(Produto p)
        {
            try
            {
                return _db.DeleteAsync(p);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover produto: {ex.Message}");
                return Task.FromResult(0);
            }
        }
    }
}