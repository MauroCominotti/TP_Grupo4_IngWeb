using RafaelaColabora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace RafaelaColabora.Services
{
    public class PostsService
    {
        private HttpClient _httpClient;
        public PostsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Post>> SearchPosts(string cadena)
        {
            //var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/Posts/Search", cadena);
            //response.EnsureSuccessStatusCode();
            //var content = await response.Content.ReadAsStringAsync();
            //var _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            //return JsonSerializer.Deserialize<List<Post>>(content, _options);

            var response = await _httpClient.GetFromJsonAsync<List<Post>>($"/Posts/Search/?cadena={cadena}");
            return response;
        }

        public async Task<Post> PostPosts(Post post)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/Posts/CreatePost", post);
            response.EnsureSuccessStatusCode();
            return post;
        }
        public async Task<List<Post>> GetPosts()
        {
             var response = await _httpClient.GetFromJsonAsync<List<Post>>("/Posts/GetPosts");
            return response;
        }

        public async Task<Post> VerifyPostLike(int postId, string userId)
        {
            var response = await _httpClient.GetFromJsonAsync<Post>($"/Posts/VerifyPostLike/{postId}/?userId={userId}");
            return response;
        }
    }
}
