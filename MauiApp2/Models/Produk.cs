using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp2.Models;

public partial class Produk : ObservableObject
{
    public int Id { get; set; }
    public string KodeProduk { get; set; } = string.Empty;

    [ObservableProperty]
    private string _namaProduk = string.Empty;

    [ObservableProperty]
    private int _stokProduk;

    [ObservableProperty]
    private string _kategoriProduk = string.Empty;

    [ObservableProperty]
    private string _statusProduk = string.Empty;
    [ObservableProperty]
    private string _waktuDibuat = string.Empty;
}