using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RafaelaColabora.Models
{
    public partial class Category
    {
        public Category()
        {
            News = new HashSet<News>();
            Posts = new HashSet<Post>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Links { get; set; }
        public byte[] Photo { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
