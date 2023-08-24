using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapKiralama.DataAccess.Abstract.DataManagement
{
    public interface IUnitOfWork
    {
        IKiralamaRepository KiralamaRepository { get; }
        IKitapRepository KitapRepository { get; }
        IKitapTuruRepository KitapTuruRepository { get; }


        Task<int> SaveChangeAsync();

    }
}
