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
    public class KiralamaMap : IEntityTypeConfiguration<Kiralama>
    {
        public void Configure(EntityTypeBuilder<Kiralama> builder)
        {
            builder.ToTable("Kiralama");
            builder.Property(y => y.OgrenciId).IsRequired();
            
        }
    }
}
