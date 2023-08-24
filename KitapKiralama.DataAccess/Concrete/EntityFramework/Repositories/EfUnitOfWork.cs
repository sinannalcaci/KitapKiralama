using KitapKiralama.DataAccess.Abstract.DataManagement;
using KitapKiralama.DataAccess.Concrete.EntityFramework.Context;
using KitapKiralama.Entity.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapKiralama.DataAccess.Concrete.EntityFramework.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public EfUnitOfWork(AppDbContext context)
        {
            _context = context;
            KiralamaRepository=new EfKiralamaRepository(_context);
            KitapRepository=new EfKitapRepository(_context);
            KitapTuruRepository=new EfKitapTuruRepository(_context);


        }

        public IKiralamaRepository KiralamaRepository { get; }

        public IKitapRepository KitapRepository { get; }

        public IKitapTuruRepository KitapTuruRepository { get; }

        public async Task<int> SaveChangeAsync()
        {
           
            return await _context.SaveChangesAsync();
        }
    }
}
