using KitapKiralama.DataAccess.Abstract.DataManagement;
using KitapKiralama.DataAccess.Concrete.EntityFramework.Repositories;
using KitapKiralama.Entity.Poco;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KitapKiralama.Web.Controllers
{
    public class KitapController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public KitapController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }

        [Authorize(Roles = "Admin,Ogrenci")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Kitap> objKitapList = await _unitOfWork.KitapRepository.GetAllAsync(null,"KitapTuru");
            return View(objKitapList);
        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        public async Task<IActionResult> EkleGuncelle(int? id)
        {

            IEnumerable<SelectListItem> KitapTuruList = (await _unitOfWork.KitapTuruRepository.GetAllAsync()).
                Select(k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString()
                });
            ViewBag.KitapTuruList = KitapTuruList;

            if (id == null || id == 0)
            {
                return View();
            }
            else
            {
                var kitapVt = await _unitOfWork.KitapRepository.GetAsync(u => u.Id == id); //Expression<Func<T, bool>> filtre
                if (kitapVt == null)
                {
                    return NotFound();
                }
                return View(kitapVt);
            }

        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public async Task<IActionResult> EkleGuncelle(Kitap kitap, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }


                if (kitap.Id == 0)
                {
                   await _unitOfWork.KitapRepository.AddAsync(kitap);
                    TempData["basarili"] = "Yeni Kitap Başarıyla Oluşturuldu!";
                }
                else
                {
                  await  _unitOfWork.KitapRepository.UpdateAsync(kitap);
                    TempData["basarili"] = "Kitap Başarıyla Güncellendi!";

                }


               await _unitOfWork.SaveChangeAsync(); //Veritabanına Ekleme İşlemi

                return RedirectToAction("Index", "Kitap");

            }
            return View();
        }



        //GET ACTİON
        [Authorize(Roles = UserRoles.Role_Admin)]
        public async Task<IActionResult> Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var kitapVt = await _unitOfWork.KitapRepository.GetAsync(u => u.Id == id);
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }


        [HttpPost, ActionName("Sil")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public async Task<IActionResult> SilPOST(int? id)
        {
            var kitap = await _unitOfWork.KitapRepository.GetAsync(u => u.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }
           await _unitOfWork.KitapRepository.RemoveAsync(kitap);
           await _unitOfWork.SaveChangeAsync();
            TempData["basarili"] = "Kitap Başarıyla Silindi!";
            return RedirectToAction("Index", "Kitap");

        }
    }
}
