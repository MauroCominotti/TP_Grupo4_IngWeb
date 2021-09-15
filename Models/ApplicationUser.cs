using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RafaelaColabora.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Claims = new HashSet<Claim>();
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            News = new HashSet<News>();
            Posts = new HashSet<Post>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cuil { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }

        public virtual ICollection<Claim> Claims { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
