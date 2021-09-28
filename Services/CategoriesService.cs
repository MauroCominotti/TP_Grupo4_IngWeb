using RafaelaColabora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RafaelaColabora.Services
{
    public class CategoriesService
    {
        private HttpClient _httpClient;
        public CategoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //public async Task<Category> PostCategories(Category category)
        //{
        //    var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/Categories/GetCategories", category);
        //    response.EnsureSuccessStatusCode();
        //    return category;
        //}

        public async Task<List<Category>> GetCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Category>>("/Categories/GetCategories");
            return response;
        }
    }
}
