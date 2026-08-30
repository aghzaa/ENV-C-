using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Models;
using MauiApp2.Services;

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
    private bool _isVisible;

    [ObservableProperty]
    private bool _isModeEdit;

    [ObservableProperty]
    private int _idYangDipilih;


    ////Collection view (Tidak di butuh kan lagi)

    //[ObservableProperty]
    //private int id;

    //[ObservableProperty]
    //private string _kodeProduk;

    //[ObservableProperty]
    //private string _namaProduk;

    //[ObservableProperty]
    //private string _kategoriProduk;

    //[ObservableProperty]
    //private int _stokProduk;

    //[ObservableProperty]
    //private string _statusProduk;

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
    private void Simpan()
    {
        DateTime sekarang = DateTime.Now;

        if (IsModeEdit == true)
        {
            if (string.IsNullOrEmpty(InputKategori) || string.IsNullOrEmpty(InputNama) || InputStok < 0 || string.IsNullOrEmpty(InputStatus))
            {
                return;
            }

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

            if (string.IsNullOrEmpty(InputKategori) || string.IsNullOrEmpty(InputNama) || InputStok < 0 || string.IsNullOrEmpty(InputStatus))
            {
                return;
            }

            int id = Products.Count + 1;


            Products.Add(new Produk
            {
                Id = id,
                KodeProduk = $"PRD{id:D3}",
                NamaProduk = InputNama,
                KategoriProduk = InputKategori,
                StokProduk = InputStok,
                StatusProduk = InputStatus,
                WaktuDibuat = sekarang.ToString("dd/MM/yyyy")
            });

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