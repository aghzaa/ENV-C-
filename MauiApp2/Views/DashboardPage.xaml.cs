using MauiApp2.ViewModels;
namespace MauiApp2;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}