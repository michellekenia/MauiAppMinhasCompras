using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

[QueryProperty(nameof(ProdutoId), "id")]
public partial class EditarProduto : ContentPage
{
    private Produto? _produto;

    public string ProdutoId
    {
        set
        {
            if (int.TryParse(value, out var id))
                _ = CarregarAsync(id);
        }
    }

    public EditarProduto()
    {
        InitializeComponent();
    }

    private async Task CarregarAsync(int id)
    {
        try
        {
            _produto = await App.Db.BuscarPorIdAsync(id);
            if (_produto != null)
            {
                txtDescricao.Text = _produto.Descricao;
                txtQuantidade.Text = _produto.Quantidade.ToString();
                txtPreco.Text = _produto.Preco.ToString("0.##");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao carregar produto: {ex.Message}", "OK");
        }
    }

    private async void OnSalvar(object sender, EventArgs e)
    {
        try
        {
            var produto = new Produto
            {
                Descricao = txtDescricao.Text?.Trim() ?? "",
                Quantidade = double.TryParse(txtQuantidade.Text, out var q) ? q : 0,
                Preco = double.TryParse(txtPreco.Text, out var p) ? p : 0
            };

            await App.Db.InserirAsync(produto);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao salvar produto: {ex.Message}", "OK");
        }
    }

    private async void OnExcluir(object sender, EventArgs e)
    {
        if (_produto is null) return;

        try
        {
            await App.Db.RemoverAsync(_produto);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao excluir produto: {ex.Message}", "OK");
        }
    }
}