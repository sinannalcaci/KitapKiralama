using KitapKiralama.Entity.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapKiralama.Entity.Poco
{
    public class Kiralama:BaseEntity
    {
        public int OgrenciId { get; set; }
        [ValidateNever]
        public int KitapId { get; set; }
        [ForeignKey("KitapId")]
        [ValidateNever]
        public Kitap? Kitap { get; set; }
    }
}
