using System.Collections.ObjectModel;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class RelatorioCategoria : ContentPage
{
    readonly ObservableCollection<CategoriaTotal> _categorias = new();

    public RelatorioCategoria()
    {
        InitializeComponent();
        CategoriasCollection.ItemsSource = _categorias;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarRelatorio();
    }

    async Task CarregarRelatorio()
    {
        try
        {
            var totais = await App.Db.GetTotalPorCategoria();

            _categorias.Clear();
            foreach (var item in totais)
                _categorias.Add(item);

            double totalGeral = totais.Sum(c => c.Total);
            TotalGeralLabel.Text = $"Total geral: R$ {totalGeral:N2}";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
