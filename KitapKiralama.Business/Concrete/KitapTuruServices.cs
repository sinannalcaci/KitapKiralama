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
    public class KitapTuruServices:IKitapTuruServices
    {
        // Var olan UNITOFWORK kullaniyoruz

        private readonly IUnitOfWork _unitOfWork;

        // Injection yapiyoruz --> Constructor Injection yaptik


        public KitapTuruServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<KitapTuru> AddAsync(KitapTuru entity)
        {
            await _unitOfWork.KitapTuruRepository.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return entity;
        }

        public async Task<IEnumerable<KitapTuru>> GetAllAsync(Expression<Func<KitapTuru, bool>> Filter = null, params string[] IncludeParameters)
        {
            return await _unitOfWork.KitapTuruRepository.GetAllAsync(Filter, IncludeParameters);
        }

        public async Task<KitapTuru> GetAsync(Expression<Func<KitapTuru, bool>> Filter, params string[] IncludeParameters)
        {
            return await _unitOfWork.KitapTuruRepository.GetAsync(Filter, IncludeParameters);
        }

        public async Task RemoveAsync(KitapTuru entity)
        {
            await _unitOfWork.KitapTuruRepository.RemoveAsync(entity);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateAsync(KitapTuru entity)
        {
            await _unitOfWork.KitapTuruRepository.UpdateAsync(entity);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
