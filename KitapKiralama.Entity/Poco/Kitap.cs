using KitapKiralama.Entity.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapKiralama.Entity.Poco
{
    public class Kitap:BaseEntity
    {
        public string KitapAdi { get; set; }
        public string Tanim { get; set; }
        public string Yazar { get; set; }
        public double Fiyat { get; set; }

        [ValidateNever]
        public int KitapTuruId { get; set; }
        [ForeignKey("KitapTuruId")]
        [ValidateNever]
        public KitapTuru KitapTuru { get; set; }
        [ValidateNever]
        public string ResimUrl { get; set; }
    }
}
