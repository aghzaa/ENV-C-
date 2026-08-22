using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp2.Models;
using System.Collections.ObjectModel;

namespace MauiApp2.ViewModels;

public partial class UserViewModel : ObservableObject
{

    [ObservableProperty]
    private ObservableCollection<User> _user = new();

    [ObservableProperty]
    private bool isFormVisible;

    [ObservableProperty]
    private string _inputName = string.Empty;

    [ObservableProperty]
    private string _inputPassword = string.Empty;

    [ObservableProperty]
    private string _inputRole = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isEditMode;

    [ObservableProperty]
    private int _idYangDiPilih;

  

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
        InputRole = user.Role;

        IsEditMode = true;
        IdYangDiPilih = user.Id;

        IsFormVisible = true;
    }

    [RelayCommand]
    private void SimpanUser()
    {
        if(IsEditMode == true)
        {

            if(InputPassword == string.Empty || InputName == string.Empty || InputRole == string.Empty)
            {
                ErrorMessage = "Semua field harus diisi.";
                return;
            }

            var userLama = User.FirstOrDefault(i => i.Id == IdYangDiPilih);

            if(userLama != null)
            {
                userLama.Username = InputName;
                userLama.Password = InputPassword;
                userLama.Role = InputRole;

                var index = User.IndexOf(userLama);
                User[index] = userLama;

            }
        }
        else
        {

        if (InputPassword == string.Empty || InputName == string.Empty || InputRole == string.Empty)
        {
            ErrorMessage = "Semua field harus diisi.";
            return;
        }
        int Nextid = User.Count + 1;
        User.Add(new User
        {
            Id = Nextid,
            Username = InputName,
            Password = InputPassword,
            Role = InputRole
        });

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
        InputRole = string.Empty;
        IsFormVisible = true;
    }

    public UserViewModel()
    {
        
    }
        

}
