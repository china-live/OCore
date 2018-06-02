using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OCore.Mvc.Admin.Models
{
    public class InstalledViewModel
    {
        [Required]
        [StringLength(255)]
        public string InstalledKey { get; set; }
    }
}
