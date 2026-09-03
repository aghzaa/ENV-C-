using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Models;
using MauiApp2.Services;

namespace MauiApp2.ViewModels;

public partial class KategoriViewModel : ObservableObject
{
    private readonly ApiServices _service;

    public KategoriViewModel(ApiServices service)
    {
        _service = service;
    }
    [ObservableProperty]
    private ObservableCollection<Kategori> _kategories = new();

    [ObservableProperty]
    private bool _isVisible;

    [ObservableProperty]
    private string _errorMessage;

    [ObservableProperty]
    private bool _modeEdit;

    [ObservableProperty]
    private int _idYangDiPilih;

    //[ObservableProperty]
    //private 

    [ObservableProperty]
    private string _inputKategori;

    [ObservableProperty]
    private string _inputKode;

    [RelayCommand]
    private async Task LoadData()
    {
        string endpoint = "http://127.0.0.1:8000/api/kategoris";

        var response = await _service.GetAllAsync<Kategori>(endpoint);

        if(response != null && response.Data != null)
        {
            Kategories.Clear();

            foreach(var i in response.Data)
            {
                Kategories.Add(i);
            }
        }
    }

    // Delete

    [RelayCommand]
    private async Task Hapus(Kategori kategori)
    {
        bool konfirmasi = await Application.Current.MainPage.DisplayAlert($"Anda yakin ingin menghapus kategori {kategori.NamaKategori}?", "konfirmasi", "Ya", "Tidak");
        if (konfirmasi)
        {
            Kategories.Remove(kategori);
        }
    }

    //Edit

    [RelayCommand]
    private void Edit(Kategori kategori)
    {
        ModeEdit = true;
        IdYangDiPilih = kategori.Id;

        resetForm();
        InputKategori = kategori.NamaKategori;
        InputKode = kategori.KodeKategori;
        IsVisible = true;
    }

    //Create

    [RelayCommand]
    private async Task Simpan()
    {

            if (string.IsNullOrEmpty(InputKategori) || string.IsNullOrEmpty(InputKode))
            {

                ErrorMessage = "Pastikan Kategori atau Kode terisi!";
                return;
            }
        if(ModeEdit == true)
        {

            var kategoriLama = Kategories.FirstOrDefault(i => i.Id == IdYangDiPilih);
            if (kategoriLama != null)
            {
                kategoriLama.NamaKategori = InputKategori;
                kategoriLama.KodeKategori = InputKode;
            }
        }
        else
        {
            string endpoint = "http://127.0.0.1:8000/api/kategoris";

            var data = new Kategori
            {
                NamaKategori = InputKategori,
                KodeKategori = InputKode
            };

            var response = await _service.PostAsync(endpoint, data);

            if (response != null && response.Status == "success")
            {
                Kategories.Add(response.Data);

                await Application.Current.MainPage.DisplayAlert("Berhasil", "Kategori berhasil ditambahkan.", "OK");

                LoadData();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Gagal", "Kategori gagal ditambahkan.", "OK");

            }

        }

        resetForm();
        IsVisible = false;
    }

    [RelayCommand]
    private void TambahKategori()
    {
        resetForm();
        IsVisible = true;
    }

    [RelayCommand]
    private void Batal()
    {
        IsVisible = false;
        resetForm();
    }

    private void resetForm()
    {
        InputKategori = string.Empty;
        InputKode = string.Empty;

        ErrorMessage = string.Empty;
    }

}
