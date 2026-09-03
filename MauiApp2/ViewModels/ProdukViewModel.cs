using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Models;
using MauiApp2.Services;
using MauiApp2.Views;

namespace MauiApp2.ViewModels;

public partial class ProdukViewModel : ObservableObject
{

    private readonly ApiServices _service;

    public ProdukViewModel(ApiServices service)
    {
        _service = service;
    }

    [ObservableProperty]
    private ObservableCollection<Produk> _products = new();

    [ObservableProperty]
    private ObservableCollection<Kategori> _kategories = new();

    [ObservableProperty]
    private Kategori _selectedKategori;

    [ObservableProperty]
    private bool _isVisible;

    [ObservableProperty]
    private bool _isModeEdit;

    [ObservableProperty]
    private int _idYangDipilih;

    //Input produk

    [ObservableProperty]
    private string _inputNama;

    [ObservableProperty]
    private string _inputKategori;

    [ObservableProperty]
    private int _inputStok;

    [ObservableProperty]
    private string _inputStatus;

    [RelayCommand]
    private async Task LoadData()
    {
        string endpoint = "http://127.0.0.1:8000/api/products";

        var response = await _service.GetAllAsync<Produk>(endpoint);

        if (response != null && response.Data != null)
        {
            Products.Clear();

            foreach(var i in response.Data)
            {
                // Konversi string datetime dari API ke format yang diinginkan
                if (DateTime.TryParse(i.WaktuDibuat, out DateTime parsedDate))
                {
                    i.WaktuDibuat = parsedDate.ToString("dd/MM/yyyy");
                }
                
                Products.Add(i);
            }
        }

        string enpointKategori = "http://127.0.0.1:8000/api/kategoris";

        var responseKategori = await _service.GetAllAsync<Kategori>(enpointKategori);

        if(responseKategori != null && responseKategori.Data != null)
        {
            Kategories.Clear();

            foreach(var i in responseKategori.Data)
            {
                Kategories.Add(i);
            }
        }
    }
     
    //fuction input

    [RelayCommand]
    private void TambahProduk()
    {
        ResetForm();
        IsModeEdit = false;
        IsVisible = true;
    }

    [RelayCommand]
    private void Batal()
    {
        ResetForm();
        IsModeEdit = false;
        IsVisible = false;
    }

    //Create

    [RelayCommand]
    private async Task Simpan()
    {
        DateTime sekarang = DateTime.Now;

        if ( string.IsNullOrEmpty(InputNama) || InputStok < 0 || string.IsNullOrEmpty(InputStatus))
        {
            return;
        }

        if (IsModeEdit == true)
        {
            

            var productLama = Products.FirstOrDefault(p => p.Id == IdYangDipilih);
            if (productLama != null)
            {
                productLama.NamaProduk = InputNama;
                productLama.StokProduk = InputStok;
                productLama.StatusProduk = InputStatus;
                productLama.KategoriProduk = InputKategori;
                productLama.WaktuDibuat = sekarang.ToString("dd/MM/yyyy");

                //var index = Products.IndexOf(productLama);
                //Produk[index] = productLama;

            }
        }
        else
        {

            string endpoint = "http://127.0.0.1:8000/api/products";

            //var kategori = SelectedKategori.Kode;

            var data = new Produk
            {
                KodeProduk = SelectedKategori.KodeKategori + "-" + Products.Count,
                NamaProduk = InputNama,
                StokProduk = InputStok,
                KategoriId = SelectedKategori.Id,
                StatusProduk = InputStatus,
            };

            var response = await _service.PostAsync<Produk>(endpoint, data);

            if (response != null && response.Status == "success")
            {
                Products.Add(response.Data);

                LoadData();

                await Application.Current.MainPage.DisplayAlert("Berhasil", "Produk berhasil di tambahkan", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Gagal", "Produk gagal di tambahkan", "OK");

            }

        }


        ResetForm();
        IsModeEdit = false;
        IsVisible = false;
    }

    //Edit

    [RelayCommand]
    private void Edit(Produk products)
    {
        InputNama = products.NamaProduk;
        InputStatus = products.StatusProduk;
        InputStok = products.StokProduk;
        InputKategori = products.KategoriProduk;

        IsModeEdit = true;
        IdYangDipilih = products.Id;

        IsVisible = true;
    }

    //Delete


    [RelayCommand]
    private async Task Hapus(Produk produk)
    {
        bool konfirmasi = await Application.Current.MainPage.DisplayAlert("Apakah anda yakin ingin menghapus produk ini?", "Konfirmasi", "Ya", "Tidak");
        if (konfirmasi)
        {
            Products.Remove(produk);
        }
    }

    private void ResetForm()
    {
        InputKategori = null;
        InputNama = string.Empty;
        InputStok = 0;
        InputStatus = string.Empty;
    }

}