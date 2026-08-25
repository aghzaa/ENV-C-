using MauiApp2.ViewModels;
using MauiApp2.Services;

namespace MauiApp2.Views;

public partial class RolePage : ContentPage
{
	public RolePage(RoleViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is  RoleViewModel vm)
		{
			vm.LoadDataCommand.Execute(null);
		}
	}
}