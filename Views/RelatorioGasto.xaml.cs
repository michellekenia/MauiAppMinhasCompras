using MauiAppMinhasCompras.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views
{
    public partial class RelatorioGasto : ContentPage
    {
        public RelatorioGasto()
        {
            InitializeComponent(); // Ensure the corresponding XAML file exists and is properly linked.
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GerarRelatorioAsync();
        }

        private async Task GerarRelatorioAsync()
        {
            var produtos = await App.Db.ListarAsync();

            var relatorio = produtos
                .GroupBy(p => p.Categoria)
                .Select(g => new
                {
                    Categoria = g.Key,
                    TotalGasto = g.Sum(p => p.Preco * p.Quantidade)
                })
                .OrderBy(r => r.Categoria)
                .ToList();

            ListaRelatorio.ItemsSource = relatorio;
        }
    }
}