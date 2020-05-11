using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgStok.Models.Entity;

namespace ArgStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        argdbstoksEntities db = new argdbstoksEntities();
        [Components.SessionAuthorization]
        public ActionResult Index()
        {
            var degerler = db.urunlers.ToList();
            return View(degerler);
        }
        [HttpGet]
        [Components.SessionAuthorization]
        public ActionResult YeniUrun()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(urunler ekle)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniUrun");
            }
            db.urunlers.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult sil(int id)
        {
            var urunsil = db.urunlers.Find(id);
            db.urunlers.Remove(urunsil);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [Components.SessionAuthorization]
        public ActionResult UrunGetir(int id)
        {
            var urun = db.urunlers.Find(id);
            return View("UrunGetir", urun);
        }
        [Components.SessionAuthorization]
        public ActionResult Guncelle(urunler p1)
        {
            var urun = db.urunlers.Find(p1.urunid);
            urun.urunkodu = p1.urunkodu;
            urun.urunadi = p1.urunadi;
            urun.urundegeri = p1.urundegeri;
            urun.urunkilifi = p1.urunkilifi;

            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}