using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MauiApp2.Models;

public class ApiResponse<T>
{

    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("success")]
    public bool? Success { get; set; }
    [JsonPropertyName("data")]
    public T? Data { get; set; }
}
