using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using System.Linq;
using System.Collections.ObjectModel;
    
namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    private ObservableCollection<Produto> _produtos;

    public ListaProduto()
    {
        InitializeComponent();
        _produtos = new ObservableCollection<Produto>();
        Lista.ItemsSource = _produtos;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarEFiltrarProdutosAsync();
    }

    private async Task CarregarEFiltrarProdutosAsync()
    {
        var textoBusca = barraBusca.Text?.ToLower() ?? "";
        var categoriaSelecionada = pckFiltroCategoria.SelectedItem?.ToString();

        try
        {
            var todosOsProdutos = await App.Db.ListarAsync();

            var produtosFiltrados = todosOsProdutos.Where(p =>
                (string.IsNullOrEmpty(categoriaSelecionada) || categoriaSelecionada == "Todos" || p.Categoria == categoriaSelecionada) &&
                (string.IsNullOrWhiteSpace(textoBusca) || p.Descricao.ToLower().Contains(textoBusca))
            ).ToList();
     
            _produtos.Clear();
            foreach (var p in produtosFiltrados)
            {
                _produtos.Add(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao carregar produtos: {ex.Message}", "OK");
        }
    }

    private async void OnFiltroChanged(object sender, EventArgs e)
    {
        await CarregarEFiltrarProdutosAsync();
    }

    private async void OnNovoClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync(nameof(NovoProduto));

    private async void OnSelecionado(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection?.FirstOrDefault() is Produto p)
        {
            await Shell.Current.GoToAsync($"{nameof(EditarProduto)}?id={p.Id}");
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    private async void OnExcluir(object sender, EventArgs e)
    {
        if ((sender as SwipeItem)?.CommandParameter is Produto p)
        {
            await App.Db.RemoverAsync(p);
            await CarregarEFiltrarProdutosAsync();
        }
    }

    private async void OnBuscaChanged(object sender, TextChangedEventArgs e)
    {
        await CarregarEFiltrarProdutosAsync();
    }

    private async void OnRelatorioClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RelatorioGasto());
    }
}