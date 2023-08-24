using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapKiralama.Entity.Poco
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public int OgrenciNo { get; set; }
        public string? Adres { get; set; }
        public string? Fakulte { get; set; }
        public string? Bolum { get; set; }

    }
}
