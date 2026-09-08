using System.Globalization;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    readonly Produto _produto;

    public EditarProduto(Produto produto)
    {
        InitializeComponent();

        _produto = produto;

        DescricaoEntry.Text = produto.Descricao;
        QuantidadeEntry.Text = produto.Quantidade.ToString(CultureInfo.CurrentCulture);
        PrecoEntry.Text = produto.Preco.ToString("N2", CultureInfo.CurrentCulture);
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

        _produto.Descricao = descricao;
        _produto.Quantidade = quantidade;
        _produto.Preco = preco;

        try
        {
            await App.Db.Update(_produto);
            await DisplayAlert("Sucesso", "Produto atualizado.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
