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
        await CarregarProdutosAsync();
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
            await CarregarProdutosAsync();
        }
    }

    private async Task CarregarProdutosAsync()
    {
        _produtos.Clear();
        var produtosDoBanco = await App.Db.ListarAsync();
        foreach (var p in produtosDoBanco)
        {
            _produtos.Add(p);
        }
    }

    private void OnBuscaChanged(object sender, TextChangedEventArgs e)
    {
        var textoBusca = barraBusca.Text?.ToLower() ?? "";

        if (string.IsNullOrWhiteSpace(textoBusca))
        {
            Lista.ItemsSource = _produtos;
        }
        else
        {
            var produtosFiltrados = _produtos
                                     .Where(p => p.Descricao.ToLower().Contains(textoBusca))
                                     .ToList();

            Lista.ItemsSource = produtosFiltrados;
        }
    }
}