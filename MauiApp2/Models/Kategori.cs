using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp2.Models;

public partial class Kategori : ObservableObject
{
    public int Id { get; set; }

    [ObservableProperty]
    private string _namaKategori = string.Empty;

    [ObservableProperty]
    private string _kodeKategori = string.Empty;
}
