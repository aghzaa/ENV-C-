using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp2.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    [RelayCommand]
    private async Task NavigateToKelolaUser()
    {
        await Shell.Current.GoToAsync("//KelolaUser");
    }
}
