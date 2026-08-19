using MauiApp2.ViewModels;

namespace MauiApp2.Views;

public partial class KelolaUser : ContentPage
{
	public KelolaUser(UserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}