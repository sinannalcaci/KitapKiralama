using KitapKiralama.DataAccess.Abstract.DataManagement;
using KitapKiralama.DataAccess.Concrete.EntityFramework.Context;
using KitapKiralama.Entity.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapKiralama.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfKitapTuruRepository : EfRepository<KitapTuru>, IKitapTuruRepository
    {
        public EfKitapTuruRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
