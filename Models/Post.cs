using RafaelaColabora.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RafaelaColabora.Models
{
    public partial class Post
    {
        public Post()
        {
            Claims = new HashSet<Claim>();
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        public string Links { get; set; }
        public string Phone { get; set; }
        public string AlternativePhone { get; set; }
        public string AlternativeEmail { get; set; }
        public byte[] Photo { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual State State { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
