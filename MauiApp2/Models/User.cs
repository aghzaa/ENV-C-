using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MauiApp2.Models;
public class User
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("role_id")]
    public int? RoleId { get; set; }
}
