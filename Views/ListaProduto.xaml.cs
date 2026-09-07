using MauiAppMinhasCompras.Models;

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
        await CarregarProdutos();
    }

    async Task CarregarProdutos()
    {
        ProdutosCollection.ItemsSource = await App.Db.GetAll();
    }

    async void BuscaBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        ProdutosCollection.ItemsSource = await App.Db.Search(e.NewTextValue ?? "");
    }

    async void Novo_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NovoProduto());
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

            await App.Db.Delete(produto.Id);
            await CarregarProdutos();
        }
    }
}
