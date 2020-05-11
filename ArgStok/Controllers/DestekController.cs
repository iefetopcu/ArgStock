using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgStok.Models.Entity;

namespace ArgStok.Controllers
{
    public class DestekController : Controller
    {
        // GET: Destek
        argdbstoksEntities db = new argdbstoksEntities();
        [Components.SessionAuthorization]
        public ActionResult Index()
        {
            var degerler = db.destektalepleris.ToList();
            return View(degerler);
        }
        [HttpGet]
        [Components.SessionAuthorization]
        public ActionResult YeniDestek()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDestek(destektalepleri p1)
        {

            var user = Session["kullaniciadi"] as kullanicilar;

            var n = new destektalepleri();

            n.konubasligi = p1.konubasligi;
            n.konuicerigi = p1.konuicerigi;
            n.uygulama = p1.uygulama;
            n.destekverilenpersonel = p1.destekverilenpersonel;
            n.il = p1.il;
            n.ilce = p1.ilce;
            n.cezaevi = p1.cezaevi;
            n.personeltelefon = p1.personeltelefon;
            n.destekveren = user.kullaniciadi + user.kullanicisoyadi;
            n.tarih = p1.tarih;
            db.destektalepleris.Add(n);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Components.SessionAuthorization]
        public ActionResult DestekGetir(int id)
        {
            var talep = db.destektalepleris.Find(id);
            return View("DestekGetir", talep);
        }

        public ActionResult Sil(int id)
        {
            var desteksil = db.destektalepleris.Find(id);
            db.destektalepleris.Remove(desteksil);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [Components.SessionAuthorization]
        public ActionResult GuncelleGetir(int id)
        {
            var destek = db.destektalepleris.Find(id);
            return View("GuncelleGetir", destek);
        }
        [Components.SessionAuthorization]
        public ActionResult Guncelle(destektalepleri p1)
        {
            var destektalepleri = db.destektalepleris.Find(p1.id);
            destektalepleri.cezaevi= p1.cezaevi;
            destektalepleri.konubasligi = p1.konubasligi;
            destektalepleri.konuicerigi = p1.konuicerigi;
            destektalepleri.destekverilenpersonel = p1.destekverilenpersonel;
            destektalepleri.ilce = p1.ilce;
            destektalepleri.il = p1.il;
            destektalepleri.uygulama = p1.uygulama;
            destektalepleri.personeltelefon = p1.personeltelefon;


            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}