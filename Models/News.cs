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
    public partial class News
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Links { get; set; }
        public string Phone { get; set; }
        public byte[] Photo { get; set; }
        public State State { get; set; }


        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
