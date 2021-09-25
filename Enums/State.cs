using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RafaelaColabora.Enums
{
    public enum State
    {
        [Display(Name = "Active")]
        Active,
        [Display(Name = "Suspended")]
        Suspended,
        [Display(Name = "Blocked")]
        Blocked
    }
}