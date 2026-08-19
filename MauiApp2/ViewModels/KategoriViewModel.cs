using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Models;

namespace MauiApp2.ViewModels;

public partial class KategoriViewModel : ObservableObject
{
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
    private void Simpan()
    {

        if(ModeEdit == true)
        {
            if (string.IsNullOrEmpty(InputKategori) || string.IsNullOrEmpty(InputKode))
            {

                ErrorMessage = "Pastikan Kategori atau Kode terisi!";
                return;
            }

            var kategoriLama = Kategories.FirstOrDefault(i => i.Id == IdYangDiPilih);
            if (kategoriLama != null)
            {
                kategoriLama.NamaKategori = InputKategori;
                kategoriLama.KodeKategori = InputKode;
            }
        }
        else
        {

            if(string.IsNullOrEmpty(InputKategori) || string.IsNullOrEmpty(InputKode))
            {
            
                ErrorMessage = "Pastikan Kategori atau Kode terisi!";
                return;
            }

            int Id = Kategories.Count + 1;

            Kategories.Add(new Kategori
            {
                Id = Id,
                NamaKategori = InputKategori,
                KodeKategori = InputKode
            });
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
