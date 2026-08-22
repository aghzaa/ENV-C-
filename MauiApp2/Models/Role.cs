using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MauiApp2.Models;
public class Role
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("role_name")]
    public string RoleName { get; set; } = string.Empty;
    [JsonPropertyName("kode")]
    public string Kode { get; set; }
}

