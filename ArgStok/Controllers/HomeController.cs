using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgStok.Models.Entity;

namespace ArgStok.Controllers
{
    public class HomeController : Controller
       
    {
        argdbstoksEntities db = new argdbstoksEntities();
        //public ActionResult Index()
        //{

        //    var degerler = db.kullanicilars.ToList();

        //    return View(degerler);

        //}

        public ActionResult Index(int? id)
        {
            if(id.HasValue)
            {
                var user = db.kullanicilars.Find(id.Value);
                ViewBag.user = user;
            }
            else
                ViewBag.user = new kullanicilar();

            var degerler = db.kullanicilars.ToList();

            return View(degerler);
        }



        [HttpPost]
        public ActionResult Index(kullanicilar model)
        {
            var KULLANICI = db.kullanicilars.FirstOrDefault(x => x.kullaniciepost == model.kullaniciepost && x.kullanicisifre == model.kullanicisifre);
            
            
            //GİRİŞ
            if (KULLANICI != null)
            {
               

              Session["kullaniciadi"] = KULLANICI;
                
                
                
              return RedirectToAction("Index","Stok");
            }
            //YALNIŞ GİRİŞ
            else
            {
                var Hatalar = " E-Posta veya şifresi yalnış";
                ViewBag.hata = Hatalar;
                return View("Index");
            }
            
        }


        // ÇIKIŞ
        public ActionResult cikis()
        {
            
            Session.Remove("kullaniciadi");
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index");
        }

    }
}