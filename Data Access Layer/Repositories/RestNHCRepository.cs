/*
using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Data_Access_Layer.Repositories
{
    public class RestNHCRepository: IStormApi
    {

        private const string NHC_URL = "https://www.nhc.noaa.gov/";
        private static readonly HttpClient _http = new HttpClient();
        // Active storms list — use directly from NHC

       public async Task<List<CycloonDTO>> GetActiveCyclonen()
        {
            string url = NHC_URL + "CurrentStorms.json";
            HttpResponseMessage response = await _http.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<List<CycloonDTO>>(content);
        }
    }
}
*/