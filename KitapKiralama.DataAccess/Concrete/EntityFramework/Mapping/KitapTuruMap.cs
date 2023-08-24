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
    public class KitapTuruMap : IEntityTypeConfiguration<KitapTuru>
    {
        public void Configure(EntityTypeBuilder<KitapTuru> builder)
        {
            builder.ToTable("KitapTuru");
            builder.Property(x=>x.Ad).HasMaxLength(100).IsRequired();
            
        }
    }
}
