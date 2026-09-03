using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp2.Models;

public partial class Produk : ObservableObject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("kode_produk")]
    public string KodeProduk { get; set; } = string.Empty;

    [ObservableProperty]
    [property: JsonPropertyName("nama_produk")]
    private string _namaProduk = string.Empty;

    [ObservableProperty]
    [property: JsonPropertyName("stok")]
    private int _stokProduk;

    [ObservableProperty]
    [property : JsonPropertyName("kategori")]
    private string _kategoriProduk = string.Empty;

    [ObservableProperty]
    [property : JsonPropertyName("status")]
    private string _statusProduk = string.Empty;

    [ObservableProperty]
    [property: JsonPropertyName("created_at")]
    private string _waktuDibuat = string.Empty;

    [JsonPropertyName("kategori_id")]
    public int KategoriId { get; set; }
}