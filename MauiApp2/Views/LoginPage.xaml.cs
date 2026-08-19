using MauiApp2.ViewModels; 
namespace MauiApp2;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	
}