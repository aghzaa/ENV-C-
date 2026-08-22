using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp2.Models;

public partial class Kategori : ObservableObject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [ObservableProperty]
    [property: JsonPropertyName("nama_kategori")]
    private string _namaKategori = string.Empty;

    [ObservableProperty]
    [property: JsonPropertyName("kode_kategori")]
    private string _kodeKategori = string.Empty;
}
