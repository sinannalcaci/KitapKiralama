using KitapKiralama.DataAccess.Abstract.DataManagement;
using KitapKiralama.Entity.Poco;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KitapKiralama.Web.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KitapTuruController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public KitapTuruController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<KitapTuru> objKitapTuruList = await _unitOfWork.KitapTuruRepository.GetAllAsync();
            return View(objKitapTuruList);
        }

        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.KitapTuruRepository.AddAsync(kitapTuru);
                await _unitOfWork.SaveChangeAsync(); //Veritabanına Ekleme İşlemi
                TempData["basarili"] = "Yeni Kitap Türü Başarıyla Oluşturuldu!";
                return RedirectToAction("Index", "KitapTuru");

            }
            return View();
        }

        public async Task<IActionResult> Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var kitapTuruVt = await _unitOfWork.KitapTuruRepository.GetAsync(x=>x.Id==id); 
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }

        [HttpPost]
        public async Task<IActionResult> Guncelle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.KitapTuruRepository.UpdateAsync(kitapTuru);
                await _unitOfWork.SaveChangeAsync(); 
                TempData["basarili"] = "Kitap Türü Başarıyla Güncellendi!";
                return RedirectToAction("Index", "KitapTuru");

            }
            return View();
        }

        //GET ACTİON
        public async Task<IActionResult> Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var kitapTuruVt = await _unitOfWork.KitapTuruRepository.GetAsync(x=>x.Id==id);
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }


        [HttpPost, ActionName("Sil")]
        public async Task<IActionResult> SilPOST(int? Id)
        {
            var kitapTuru = await _unitOfWork.KitapTuruRepository.GetAsync(u => u.Id == Id);
            if (kitapTuru == null)
            {
                return NotFound();
            }
            await _unitOfWork.KitapTuruRepository.RemoveAsync(kitapTuru);
            await _unitOfWork.SaveChangeAsync();
            TempData["basarili"] = "Kitap Türü Başarıyla Silindi!";
            return RedirectToAction("Index", "KitapTuru");

        }

    }
}
