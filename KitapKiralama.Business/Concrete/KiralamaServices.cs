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
    public class KiralamaServices : IKiralamaServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public KiralamaServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Kiralama> AddAsync(Kiralama entity)
        {
            await _unitOfWork.KiralamaRepository.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return entity;
        }

        public async Task<IEnumerable<Kiralama>> GetAllAsync(Expression<Func<Kiralama, bool>> Filter = null, params string[] IncludeParameters)
        {
            var list = await _unitOfWork.KiralamaRepository.GetAllAsync(Filter, IncludeParameters);
            
            return list;
        }

        public async Task<Kiralama> GetAsync(Expression<Func<Kiralama, bool>> Filter, params string[] IncludeParameters)
        {
            return await _unitOfWork.KiralamaRepository.GetAsync(Filter, IncludeParameters);
        }

        public async Task RemoveAsync(Kiralama entity)
        {
            await _unitOfWork.KiralamaRepository.RemoveAsync(entity);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateAsync(Kiralama entity)
        {
            await _unitOfWork.KiralamaRepository.UpdateAsync(entity);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
