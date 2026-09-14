using System.Collections.ObjectModel;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    readonly ObservableCollection<Produto> _produtos = new();

    public ListaProduto()
    {
        InitializeComponent();
        ProdutosCollection.ItemsSource = _produtos;

        var opcoes = new List<string> { Categorias.Todas };
        opcoes.AddRange(Categorias.Lista);
        CategoriaPicker.ItemsSource = opcoes;
        CategoriaPicker.SelectedIndex = 0;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await AplicarFiltros();
    }

    async Task AplicarFiltros()
    {
        try
        {
            string texto = BuscaBar.Text ?? "";
            string? categoria = CategoriaPicker.SelectedItem as string;
            AtualizarColecao(await App.Db.Search(texto, categoria));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    async void BuscaBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        await AplicarFiltros();
    }

    async void CategoriaPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        await AplicarFiltros();
    }

    void AtualizarColecao(List<Produto> produtos)
    {
        _produtos.Clear();
        foreach (var produto in produtos)
            _produtos.Add(produto);
    }

    async void Novo_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NovoProduto());
    }

    async void Relatorio_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RelatorioCategoria());
    }

    async void Editar_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Produto produto)
            await Navigation.PushAsync(new EditarProduto(produto));
    }

    async void Excluir_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Produto produto)
        {
            bool confirmar = await DisplayAlert(
                "Excluir produto",
                $"Deseja excluir \"{produto.Descricao}\"?",
                "Sim",
                "Não");

            if (!confirmar)
                return;

            try
            {
                await App.Db.Delete(produto.Id);
                await AplicarFiltros();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}
