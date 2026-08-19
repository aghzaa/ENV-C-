using MauiApp2.ViewModels;
namespace MauiApp2.Views;

public partial class Product : ContentPage
{
	public Product(ProdukViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}