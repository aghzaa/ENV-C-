using MauiApp2.ViewModels;

namespace MauiApp2.Views;

public partial class KelolaUser : ContentPage
{
	public KelolaUser(UserViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Pastikan halaman ini tahu bahwa ia terikat dengan ProdukViewModel
        if (BindingContext is UserViewModel viewModel)
        {
            // Jalankan perintah LoadData yang baru saja kita buat
            viewModel.loadDataCommand.Execute(null);
        }
    }
}