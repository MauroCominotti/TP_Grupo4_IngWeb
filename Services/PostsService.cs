using RafaelaColabora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<Post> PostPosts(Post post)
        {
            //var jsonInString = Newtonsoft.Json.JsonConvert.SerializeObject(post);
            var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/Posts/CreatePost", post);
            response.EnsureSuccessStatusCode();
            return post;
        }
        public async Task<List<Post>> GetPosts()
        {
             var response = await _httpClient.GetFromJsonAsync<List<Post>>("/Posts/GetPosts");
            return response;
        }
    }
}
