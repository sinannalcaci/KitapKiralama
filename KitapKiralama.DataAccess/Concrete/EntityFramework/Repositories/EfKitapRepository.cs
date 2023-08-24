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
    public class EfKitapRepository : EfRepository<Kitap>, IKitapRepository
    {
        public EfKitapRepository(AppDbContext context) : base(context)
        {
        }
    }
}
