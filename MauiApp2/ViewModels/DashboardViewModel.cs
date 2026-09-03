using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using MauiApp2.Models;
using MauiApp2.Services;

namespace MauiApp2.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    [RelayCommand]
    private async Task NavigateToKelolaUser()
    {
        await Shell.Current.GoToAsync("//KelolaUser");
    }

    private readonly ApiServices _service;

    public DashboardViewModel(ApiServices service)
    {
        _service = service;
    }


    [ObservableProperty]
    private ObservableCollection<User> _users = new();

    [RelayCommand]
    private async Task GetUser()
    {
        string endpoint = "http://127.0.0.1:8000/api/users";

        var response = await _service.GetAllAsync<User>(endpoint);

        if (response != null && response.Data != null)
        {
            Users.Clear();

            foreach(var i in response.Data)
            {
                Users.Add(i);
            }
        }
    }
}
