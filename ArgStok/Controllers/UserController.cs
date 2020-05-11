using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgStok.Models.Entity;

namespace ArgStok.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        argdbstoksEntities db = new argdbstoksEntities();
        [Components.SessionAuthorization]
        public ActionResult Index()
        {
            var degerler = db.kullanicilars.ToList();
            return View(degerler);
        }
        [HttpGet]
        [Components.SessionAuthorization]
        public ActionResult YeniKullanici()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKullanici(kullanicilar ekle)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKullanici");
            }
            db.kullanicilars.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult sil(int id)
        {
            var kullanici = db.kullanicilars.Find(id);
            db.kullanicilars.Remove(kullanici);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [Components.SessionAuthorization]
        public  ActionResult KullaniciGetir(int id)
        {
            var user = db.kullanicilars.Find(id);
            return View("KullaniciGetir", user);
        }
        [Components.SessionAuthorization]
        public ActionResult Guncelle(kullanicilar p1)
        {
            var kullanici = db.kullanicilars.Find(p1.id);
            kullanici.kullaniciadi = p1.kullaniciadi;
            kullanici.kullanicisoyadi = p1.kullanicisoyadi;
            kullanici.kullaniciepost = p1.kullaniciepost;
            kullanici.kullanicisifre = p1.kullanicisifre;
            kullanici.yetki = p1.yetki;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}