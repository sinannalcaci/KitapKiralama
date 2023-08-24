using KitapKiralama.Business.Abstract;
using KitapKiralama.DataAccess.Abstract.DataManagement;
using KitapKiralama.DataAccess.Concrete.EntityFramework.Repositories;
using KitapKiralama.Entity.Poco;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KitapKiralama.Web.Controllers
{
    public class KiralamaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKiralamaServices _kiralamaServices;

        public KiralamaController(IUnitOfWork unitOfWork, IKiralamaServices kiralamaServices)
        {
            _unitOfWork = unitOfWork;
            _kiralamaServices = kiralamaServices;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Kiralama> objKiralamaList = await _kiralamaServices.GetAllAsync(null,"Kitap");

            return View(objKiralamaList.ToList());
        }

        public async Task<IActionResult> EkleGuncelle(int? id)
        {

            IEnumerable<SelectListItem> KitapList = (await _unitOfWork.KitapRepository.GetAllAsync())
                .Select(k => new SelectListItem
                {
                    Text = k.KitapAdi,
                    Value = k.Id.ToString()
                });
            ViewBag.KitapList = KitapList;

            if (id == null || id == 0)
            {
                //ekle
                return View();
            }
            else
            {
                //guncelle
                var kiralamaVt = await _unitOfWork.KiralamaRepository.GetAsync(u => u.Id == id); //Expression<Func<T, bool>> filtre
                if (kiralamaVt == null)
                {
                    return NotFound();
                }
                return View(kiralamaVt);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EkleGuncelle(Kiralama kiralama)
        {
            if (ModelState.IsValid)
            {


                if (kiralama.Id == 0)
                {
                    await _unitOfWork.KiralamaRepository.AddAsync(kiralama);
                    TempData["basarili"] = "Yeni Kiralama Kaydı Başarıyla Oluşturuldu!";
                }
                else
                {
                    await _unitOfWork.KiralamaRepository.UpdateAsync(kiralama);
                    TempData["basarili"] = "Kiralama Başarıyla Güncellendi!";

                }


                await _unitOfWork.SaveChangeAsync(); //Veritabanına Ekleme İşlemi

                return RedirectToAction("Index", "Kiralama");

            }
            return View();
        }

        //GETACTİON
        public async Task<IActionResult> Sil(int? id)
        {

            IEnumerable<SelectListItem> KitapList = (await _unitOfWork.KitapRepository.GetAllAsync())
               .Select(k => new SelectListItem
               {
                   Text = k.KitapAdi,
                   Value = k.Id.ToString()
               });
            ViewBag.KitapList = KitapList;


            if (id == null || id == 0)
            {
                return NotFound();
            }
            var kiralamaVt = await _unitOfWork.KiralamaRepository.GetAsync(u => u.Id == id);
            if (kiralamaVt == null)
            {
                return NotFound();
            }
            return View(kiralamaVt);
        }

        [HttpPost, ActionName("Sil")]
        public async Task<IActionResult> SilPOST(int? id)
        {
            var kiralama = await _unitOfWork.KiralamaRepository.GetAsync(u => u.Id == id);
            if (kiralama == null)
            {
                return NotFound();
            }
            await _unitOfWork.KiralamaRepository.RemoveAsync(kiralama);
            await _unitOfWork.SaveChangeAsync();
            TempData["basarili"] = "Kayıt Başarıyla Silindi!";
            return RedirectToAction("Index", "Kiralama");

        }

    }
}
