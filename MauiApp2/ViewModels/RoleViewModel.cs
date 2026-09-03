using System;
using System.Collections.Generic;
using System.Text;
using MauiApp2.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using MauiApp2.Services;

namespace MauiApp2.ViewModels;

public partial class RoleViewModel : ObservableObject
{
    private readonly ApiServices _services;

    public RoleViewModel(ApiServices service)
    {
        _services = service;
    }

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
    private async Task LoadData()
    {
        string endpoint = "http://127.0.0.1:8000/api/roles";

        var ApiResponse = await _services.GetAllAsync<Role>(endpoint);

        if(ApiResponse != null && ApiResponse.Data != null)
        {
            Roles.Clear();

            foreach(var i in ApiResponse.Data)
            {
                Roles.Add(i);
            }
        }

    }

    [RelayCommand]
    private async Task HapusRole(Role role)
    {
        var IsDelete = await Application.Current.MainPage.DisplayAlert("Konfirmasi", $"Apakah anda yakin ingin mengapus role {role.RoleName}", "Ya", "Tidak");
        
        if (!IsDelete) return; 

         string endpoint = $"http://127.0.0.1:8000/api/roles/{role.Id}";

         bool response = await _services.DeleteAsync(endpoint);

        if (response)
        {
            Roles.Remove(role);

            await Application.Current.MainPage.DisplayAlert("Berhasil", "Data Berhasil di hapus", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Gagal", "Data gagal di hapus", "OK");

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
    private async Task Simpan()
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

                IsFormVisible = false;
            }
        }else
        {

        if(string.IsNullOrWhiteSpace(InputRole) || string.IsNullOrWhiteSpace(InputKode))
        {
            return;
        }

            var data = new Role
            {
                RoleName = InputRole,
                Kode = InputKode,
            };

            string endpoint = "http://127.0.0.1:8000/api/roles";

            var response =await _services.PostAsync<Role>(endpoint, data);

            if (response != null && response.Status == "success")
            {
                Roles.Add(response.Data);

                IsFormVisible = false;
                await Application.Current.MainPage.DisplayAlert("Berhasil", "Data berhasil di tambahkan", "Ya");

            }
            else
            {
                IsFormVisible = false;
                await Application.Current.MainPage.DisplayAlert("Gagal", "Data gagal di simpan ke Server", "Ya");
            }

        }

        InputRole = string.Empty;
        InputKode = string.Empty;
        //IsFormVisible = false;

    }
    [RelayCommand]
    private void Batal()
    {
        IsFormVisible = false;
    }

}