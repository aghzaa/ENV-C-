using System;
using MauiApp2.ViewModels;
namespace MauiApp2.Views;

public partial class KelolaKategori : ContentPage
{
	public KelolaKategori(KategoriViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}