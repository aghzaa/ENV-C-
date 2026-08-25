using MauiApp2.ViewModels;
namespace MauiApp2.Views;

public partial class Product : ContentPage
{
	public Product(ProdukViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

		if(BindingContext is ProdukViewModel vm)
		{
			vm.LoadDataCommand.Execute(null);
		}
    }
}