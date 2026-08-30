using System;
using System.Collections.Generic;
using System.Text;
using MauiApp2.Models;
using System.Text.Json;
using System.Globalization;
//using Android.Media.TV;

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
            Application.Current.MainPage.DisplayAlert("Terjadi error : " + ex, "Error", "Kembali");
        }

        return null;
    }

    public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, T data)
    {
        try
        {
            //ubah object C# jadi json
            var json = JsonSerializer.Serialize(data);
            //ubah jsonmentah menjadi string
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //kirim request ke server
            var response = await _httpClient.PostAsync(endpoint, content);
            //validasi status response
            if (response.IsSuccessStatusCode)
            {   //baca response dari server
                var jsonResult = await response.Content.ReadAsStringAsync();
                //ubah response server menjadi object c#
                var dataJson = JsonSerializer.Deserialize<ApiResponse<T>>(jsonResult);

                return dataJson;
            }

        }catch(Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("Terjadi Error :" + ex, "Error", "Ya");
        }

        return null;
    }
}
