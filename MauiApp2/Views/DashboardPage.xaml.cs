using MauiApp2.ViewModels;
namespace MauiApp2;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is DashboardViewModel vm)
        {
             vm.GetUserCommand.ExecuteAsync(null);
        }
    }

}