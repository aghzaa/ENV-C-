using MauiApp2.ViewModels;

namespace MauiApp2.Views;

public partial class RolePage : ContentPage
{
	public RolePage(RoleViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}