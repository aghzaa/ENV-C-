using System;
using System.Collections.Generic;
using System.Text;
using MauiApp2.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MauiApp2.ViewModels;

public partial class RoleViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Role> _roles = new();

    [ObservableProperty]
    private string _inputRole;

    [ObservableProperty]
    private string _inputKode;

    [ObservableProperty]
    private bool _isFormVisible = false;

    [ObservableProperty]
    private bool _isEditMode;

    [ObservableProperty]
    private int _idYangDiPilih;

    [RelayCommand]
    private async Task HapusRole(Role role)
    {
        var IsDelete = await Application.Current.MainPage.DisplayAlert("Konfirmasi", $"Apakah anda yakin ingin mengapus role {role.RoleName}", "Ya", "Tidak");
        if (IsDelete)
        {
            Roles.Remove(role);
        }
    }

    [RelayCommand]
    private void EditRole(Role Roles)
    {
        InputRole = Roles.RoleName;
        InputKode = Roles.Kode;

        IdYangDiPilih = Roles.Id;
        IsEditMode = true;

        IsFormVisible = true;
    }

    [RelayCommand]
    private void TambahRole()
    {
        InputRole = string.Empty;
        InputKode = string.Empty;
        IsFormVisible = true;
    }

    [RelayCommand]
    private void Simpan()
    {
        if(IsEditMode == true)
        {
            if(string.IsNullOrEmpty(InputRole) || string.IsNullOrEmpty(InputKode))
            {
                return;
            }

            var roleLama = Roles.FirstOrDefault(r => r.Id == IdYangDiPilih);
            if(roleLama != null)
            {
                roleLama.RoleName = InputRole;
                roleLama.Kode = InputKode;

                var index = Roles.IndexOf(roleLama);
                Roles[index] = roleLama;
            }
        }else
        {

        if(string.IsNullOrWhiteSpace(InputRole) || string.IsNullOrWhiteSpace(InputKode))
        {
            return;
        }

        int id = Roles.Count + 1;
        Roles.Add(new Role
        {
            Id = id,
            RoleName = InputRole,
            Kode = InputKode
        });

        }

        IsFormVisible = false;

    }
    [RelayCommand]
    private void Batal()
    {
        IsFormVisible = false;
    }

}