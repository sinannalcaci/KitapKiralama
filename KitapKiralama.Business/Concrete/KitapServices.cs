using KitapKiralama.Business.Abstract;
using KitapKiralama.DataAccess.Abstract.DataManagement;
using KitapKiralama.Entity.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KitapKiralama.Business.Concrete
{
    public class KitapServices:IKitapServices
    {
        // Var olan UNITOFWORK kullaniyoruz

        private readonly IUnitOfWork _unitOfWork;

        // Injection yapiyoruz --> Constructor Injection yaptik

        public KitapServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Kitap> AddAsync(Kitap entity)
        {
            await _unitOfWork.KitapRepository.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return entity;
        }

        public async Task<IEnumerable<Kitap>> GetAllAsync(Expression<Func<Kitap, bool>> Filter = null, params string[] IncludeParameters)
        {
            return await _unitOfWork.KitapRepository.GetAllAsync(Filter, IncludeParameters);
        }

        public async Task<Kitap> GetAsync(Expression<Func<Kitap, bool>> Filter, params string[] IncludeParameters)
        {
            return await _unitOfWork.KitapRepository.GetAsync(Filter, IncludeParameters);
        }

        public async Task RemoveAsync(Kitap entity)
        {
            await _unitOfWork.KitapRepository.RemoveAsync(entity);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateAsync(Kitap entity)
        {
            await _unitOfWork.KitapRepository.UpdateAsync(entity);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
