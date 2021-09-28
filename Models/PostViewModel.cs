using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RafaelaColabora.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Links { get; set; }
        public string Phone { get; set; }
        public string AlternativePhone { get; set; }
        public string AlternativeEmail { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
