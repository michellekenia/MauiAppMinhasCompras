using System;
using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Helpers;
using Microsoft.Maui.Controls;

namespace MauiAppMinhasCompras.Views
{
    [QueryProperty(nameof(Id), "id")]
    public partial class NovoProduto : ContentPage
    {
        private Produto? _produto;
        public string? Id { get; set; }

        public NovoProduto()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

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
}