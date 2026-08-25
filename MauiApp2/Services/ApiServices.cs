using System;
using System.Collections.Generic;
using System.Text;
using MauiApp2.Models;
using System.Text.Json;
using System.Globalization;

namespace MauiApp2.Services;

public class ApiServices
{
    private readonly HttpClient _httpClient;

    public ApiServices()
    {
        _httpClient = new HttpClient();
    }

    public async Task<ApiResponse<List<T?>>> GetAllAsync<T>(string endpoint)
    {
        try
        {
            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<ApiResponse<List<T>>>(jsonResult);

                return data;
            }
        }
        catch(Exception ex)
        {

        }

        return null;
    }
}
