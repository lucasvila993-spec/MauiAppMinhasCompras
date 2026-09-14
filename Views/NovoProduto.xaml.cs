using System.Globalization;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
        CategoriaPicker.ItemsSource = Categorias.Lista;
        CategoriaPicker.SelectedIndex = 0;
    }

    async void Salvar_Clicked(object sender, EventArgs e)
    {
        string descricao = DescricaoEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(descricao))
        {
            await DisplayAlert("Atenção", "Informe a descrição do produto.", "OK");
            return;
        }

        if (!double.TryParse(QuantidadeEntry.Text, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double quantidade) || quantidade <= 0)
        {
            await DisplayAlert("Atenção", "Informe uma quantidade válida.", "OK");
            return;
        }

        if (!double.TryParse(PrecoEntry.Text, NumberStyles.Any,
                CultureInfo.CurrentCulture, out double preco) || preco < 0)
        {
            await DisplayAlert("Atenção", "Informe um preço válido.", "OK");
            return;
        }

        var produto = new Produto
        {
            Descricao = descricao,
            Quantidade = quantidade,
            Preco = preco,
            Categoria = CategoriaPicker.SelectedItem as string ?? "Outros"
        };

        try
        {
            await App.Db.Insert(produto);
            await DisplayAlert("Sucesso", "Produto cadastrado.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
