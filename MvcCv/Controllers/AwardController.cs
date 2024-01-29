using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcCv.Models.Entity;
using MvcCv.Repositories;

namespace MvcCv.Controllers
{
    public class AwardController : Controller
    {
        // GET: Award
        GenericRepository<TblAward> repo = new GenericRepository<TblAward>();
        [Authorize]
        public ActionResult Index()
        {
            var sertifika = repo.List();
            return View(sertifika);
        }
        [HttpGet]
        public ActionResult AwardUpdate(int id)
        {
            ViewBag.d = id;
            var sertifika = repo.Fınd(x => x.AwarID == id);
            return View(sertifika);
        }
        [HttpPost]
        public ActionResult AwardUpdate(TblAward tblAward)
        {
            var sertifika = repo.Fınd(x => x.AwarID == tblAward.AwarID);

            // sertifika'nın null olup olmadığını kontrol et
            if (sertifika != null)
            {
                // sertifika null değilse, özelliklerini güncelle
                sertifika.explanation = tblAward.explanation;
                sertifika.Date = tblAward.Date;
                repo.TUpdate(sertifika);
            }
            else
            {
                // sertifika null ise, uygun bir işlem yap
                // Örneğin bir hata mesajı döndürmek veya kullanıcıyı bilgilendirmek
                // Burada basit bir ViewBag mesajı ekleniyor
                ViewBag.ErrorMessage = "Güncellenecek sertifika bulunamadı.";
                return View(tblAward);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AwardAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AwardAdd(TblAward tblAward)
        {
            repo.TAdd(tblAward);
            return RedirectToAction("Index");
        }
        public ActionResult AwardDelete(int id)
        {
            var values = repo.Fınd(x => x.AwarID == id);
            repo.TDelete(values);
            return RedirectToAction("Index");
        }
    }
}