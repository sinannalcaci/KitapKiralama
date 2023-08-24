using KitapKiralama.Entity.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapKiralama.DataAccess.Concrete.EntityFramework.Mapping
{
    internal class KitapMap : IEntityTypeConfiguration<Kitap>
    {
        public void Configure(EntityTypeBuilder<Kitap> builder)
        {
            builder.ToTable("Kitap");
            builder.Property(k => k.KitapAdi).HasMaxLength(100).IsRequired();
            builder.Property(k => k.Yazar).HasMaxLength(100).IsRequired();
            builder.Property(k => k.Yazar).HasMaxLength(100).IsRequired();
            builder.Property(k=>k.Fiyat).IsRequired();
            

        }
    }
}
