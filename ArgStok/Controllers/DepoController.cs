using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgStok.Models.Entity;


namespace ArgStok.Controllers
{
    public class DepoController : Controller
    {
        // GET: Depo
        argdbstoksEntities db = new argdbstoksEntities();
        [Components.SessionAuthorization]
        public ActionResult Index()
        {
            var degerler = db.depoes.ToList();
            return View(degerler);

        }
        [HttpGet]
        [Components.SessionAuthorization]
        public ActionResult YeniDepo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniDepo(depo ekle)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniDepo");
            }
            db.depoes.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var deposil = db.depoes.Find(id);
            db.depoes.Remove(deposil);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [Components.SessionAuthorization]
        public ActionResult DepoGetir(int id)
        {
            var depo = db.depoes.Find(id);
            return View("DepoGetir", depo);
        }
        [Components.SessionAuthorization]
        public ActionResult Guncelle(depo p1)
        {
            var depo = db.depoes.Find(p1.depoid);
            depo.depoadi = p1.depoadi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}