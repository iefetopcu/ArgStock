using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgStok.Models.Entity;

namespace ArgStok.Controllers
{
    public class RafController : Controller
    {
        // GET: Raf
        argdbstoksEntities db = new argdbstoksEntities();
        [Components.SessionAuthorization]
        public ActionResult Index()
        {
            var degerler = db.rafs.ToList();
            return View(degerler);
        }
        [HttpGet]
        [Components.SessionAuthorization]
        public ActionResult YeniRaf()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniRaf(raf ekle)
        {
            
            db.rafs.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var rafsil = db.rafs.Find(id);
            db.rafs.Remove(rafsil);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [Components.SessionAuthorization]
        public ActionResult RafGetir(int id)
        {
            var raf = db.rafs.Find(id);
            return View("RafGetir", raf);
        }
        [Components.SessionAuthorization]
        public ActionResult Guncelle(raf p1)
        {
            var raf = db.rafs.Find(p1.rafid);
            raf.rafid = p1.rafid;
            raf.rafadi = p1.rafadi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}