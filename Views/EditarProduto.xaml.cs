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
        _produto = await App.Db.BuscarPorIdAsync(id);
        if (_produto != null)
        {
            txtDescricao.Text = _produto.Descricao;
            txtQuantidade.Text = _produto.Quantidade.ToString();
            txtPreco.Text = _produto.Preco.ToString("0.##");
        }
    }

    private async void OnSalvar(object sender, EventArgs e)
    {
        if (_produto is null) return;

        _produto.Descricao = txtDescricao.Text?.Trim() ?? "";
        _produto.Quantidade = double.TryParse(txtQuantidade.Text, out var q) ? q : 0;
        _produto.Preco = double.TryParse(txtPreco.Text, out var p) ? p : 0;

        await App.Db.AtualizarAsync(_produto);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnExcluir(object sender, EventArgs e)
    {
        if (_produto is null) return;
        await App.Db.RemoverAsync(_produto);
        await Shell.Current.GoToAsync("..");
    }
}