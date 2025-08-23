using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using System.Linq;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    public ListaProduto()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Lista.ItemsSource = await App.Db.ListarAsync();
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
            Lista.ItemsSource = await App.Db.ListarAsync();
        }
    }
}