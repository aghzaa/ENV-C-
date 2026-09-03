using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Models;
using MauiApp2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MauiApp2.ViewModels;

public partial class UserViewModel : ObservableObject
{
    private readonly ApiServices _apiServices;
    public UserViewModel(ApiServices apiservice)
    {
        _apiServices = apiservice;
    }

    [ObservableProperty]
    private ObservableCollection<Role> _roles = new();

    [ObservableProperty]
    private Role _selectedRole;

    [ObservableProperty]
    private ObservableCollection<User> _user = new();

    [ObservableProperty]
    private bool isFormVisible;

    [ObservableProperty]
    private string _inputName = string.Empty;

    [ObservableProperty]
    private string _inputPassword = string.Empty;

    [ObservableProperty]
    private int _inputRole;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isEditMode;

    [ObservableProperty]
    private int _idYangDiPilih;


    [RelayCommand]
    private async Task loadDataAsync()
    {
        string endpoint = "http://127.0.0.1:8000/api/users";

        var response = await _apiServices.GetAllAsync<User>(endpoint);

        if (response != null && response.Data != null)
        {
            User.Clear();

            foreach(var i in response.Data)
            {
                User.Add(i);
            }
        }

        string urlRole = "http://127.0.0.1:8000/api/roles";

        var apiResponse = await _apiServices.GetAllAsync<Role>(urlRole);

        if(apiResponse != null && apiResponse.Data != null)
        {
            Roles.Clear();

            foreach(var i in apiResponse.Data)
            {
                Roles.Add(i);
            }
        }
    }


    [RelayCommand]
    private void BatalForm()
    {
        IsFormVisible = false;
    }

    [RelayCommand]
    private async Task Hapus(User user)
    {
     bool IsHapus = await Application.Current.MainPage.DisplayAlert("Konfirmasi", "Apakah Anda yakin ingin menghapus data ini?", "Ya", "Tidak");
        if (IsHapus)
        {

        User.Remove(user);
        }
    }

    [RelayCommand]
    private void EditUser(User user)
    {
        InputName = user.Username;
        InputRole = int.Parse(user.Role);

        IsEditMode = true;
        IdYangDiPilih = user.Id;

        IsFormVisible = true;
    }

    [RelayCommand]
    private async Task SimpanUser()
    {
        if(InputPassword == string.Empty || InputName == string.Empty || SelectedRole ==null)
        {
            ErrorMessage = "Semua field harus diisi.";
            return;
        }
        if(IsEditMode == true)
        {
            var userLama = User.FirstOrDefault(i => i.Id == IdYangDiPilih);
            int roleid = SelectedRole.Id;

            if (userLama != null)
            {
                userLama.Username = InputName;
                userLama.Password = InputPassword;
                userLama.RoleId = roleid;

                var index = User.IndexOf(userLama);
                User[index] = userLama;

            }
        }
        else
        {

            string endpoint = "http://127.0.0.1:8000/api/users";

            var data = new User
            {
                Username = InputName,
                Password = InputPassword,
                RoleId = SelectedRole.Id,
            };

            var response = await _apiServices.PostAsync<User>(endpoint, data);

            if (response != null && response.Status == "success")
            {
                User.Add(response.Data);

                await Application.Current.MainPage.DisplayAlert("Berhasil", "Berhasil menambahkan user", "Oke");

                await loadDataAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Gagal", "Data gagal di simpan ke Server: ", "Ya");
            }

        }

        IsFormVisible = false;
        IsEditMode = false;
    }

    [RelayCommand]
    private async Task TambahUser()
    {
        InputName = string.Empty;
        InputPassword = string.Empty;
        ErrorMessage = string.Empty;
        SelectedRole = null;
        IsFormVisible = true;
    }

    public UserViewModel()
    {
        
    }
        

}
