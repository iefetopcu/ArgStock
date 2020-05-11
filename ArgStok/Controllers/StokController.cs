using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgStok.Models.Entity;
using Newtonsoft.Json;
using OfficeOpenXml;
//using PagedList;
//using PagedList.Mvc;


namespace ArgStok.Controllers
{
    public class StokController : Controller
    {
        private readonly argdbstoksEntities db;
        public StokController()
        {
            db = new argdbstoksEntities();
        }

        [Components.SessionAuthorization]
        public ActionResult Index(int sayfa=1)
        {
            //var deger = db.stoks.ToList();


            //List<SelectListItem> degerler = (from i in db.urunlers.ToList()
            //                                 select new SelectListItem
            //                                 {
            //                                     Text = i.urunadi,
            //                                     Value = i.urunid.ToString()
            //                                 }).ToList();
            //List<SelectListItem> depolar = (from d in db.depoes.ToList()
            //                                select new SelectListItem
            //                                {
            //                                    Text = d.depoadi,
            //                                    Value = d.depoid.ToString()
            //                                }).ToList();
            //List<SelectListItem> raflar = (from r in db.depoadres.ToList()
            //                               select new SelectListItem
            //                               {
            //                                   Text = r.depoadres,
            //                                   Value = r.depoadresid.ToString()
            //                               }).ToList();
            //ViewBag.raf = raflar;
            //ViewBag.dgr = degerler;
            //ViewBag.depo = depolar;

            var deger = db.stoks.ToList();

            return View(deger);
            
        }

        [Components.SessionAuthorization]
        [HttpGet]
        public ActionResult YeniStok()
        {
            List<SelectListItem> degerler = (from i in db.urunlers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.urunadi +=i.urundegeri,
                                                 Value = i.urunid.ToString()
                                             }).ToList();
            List<SelectListItem> depolar = (from d in db.depoes.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = d.depoadi,
                                                 Value = d.depoid.ToString()
                                             }).ToList();
            List<SelectListItem> raflar = (from r in db.rafs.ToList()
                                           select new SelectListItem
                                           {
                                               Text = r.rafadi,
                                               Value = r.rafid.ToString()
                                           }).ToList();
            ViewBag.raf = raflar;
            ViewBag.dgr = degerler;
            ViewBag.depo = depolar;
           

           
            return View();
        }
        [Components.SessionAuthorization]
        [HttpPost]

        
        public ActionResult YeniStok(stok ekle)
        {
            var ktg = db.urunlers.Where(m => m.urunid == ekle.urunler.urunid).FirstOrDefault();
            ekle.urunler = ktg;
            db.stoks.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [Components.SessionAuthorization]
        public ActionResult StokGirdi(int id)
        {
            List<SelectListItem> degerler = (from i in db.urunlers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.urunadi,
                                                 Value = i.urunid.ToString()
                                             }).ToList();
            List<SelectListItem> depolar = (from d in db.depoes.ToList()
                                            select new SelectListItem
                                            {
                                                Text = d.depoadi,
                                                Value = d.depoid.ToString()
                                            }).ToList();
            List<SelectListItem> raflar = (from r in db.rafs.ToList()
                                           select new SelectListItem
                                           {
                                               Text = r.rafadi,
                                               Value = r.rafid.ToString()
                                           }).ToList();
            ViewBag.raf = raflar;
            ViewBag.dgr = degerler;
            ViewBag.depo = depolar;

            
            var stokgetir = db.stoks.Find(id);
            return View("StokGirdi", stokgetir);
        }
        [Components.SessionAuthorization]
        public ActionResult StokCik(int id)
        {
            List<SelectListItem> degerler = (from i in db.urunlers.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.urunadi,
                                                 Value = i.urunid.ToString()
                                             }).ToList();
            List<SelectListItem> depolar = (from d in db.depoes.ToList()
                                            select new SelectListItem
                                            {
                                                Text = d.depoadi,
                                                Value = d.depoid.ToString()
                                            }).ToList();
            List<SelectListItem> raflar = (from r in db.rafs.ToList()
                                           select new SelectListItem
                                           {
                                               Text = r.rafadi,
                                               Value = r.rafid.ToString()
                                           }).ToList();
            ViewBag.raf = raflar;
            ViewBag.dgr = degerler;
            ViewBag.depo = depolar;


            var stokcik = db.stoks.Find(id);
            return View("StokCik", stokcik);
        }
        public ActionResult StokEkle(int id, int miktar)
        {
            var stok = db.stoks.FirstOrDefault(a => a.id == id);
            stok.adet = stok.adet + miktar;

            var user = Session["kullaniciadi"] as kullanicilar;

            var s = new log();
            s.urunadi = stok.urunler.urunadi;
            s.urunkodu = stok.urunler.urunkodu;
            s.urundegeri = stok.urunler.urundegeri;
            s.urunkilifi = stok.urunler.urunkilifi;
            s.depo = stok.depo1.depoadi;
            s.raf = stok.raf1.rafadi;
            s.girdicikti = 1;
            s.islemyapan = user.kullaniciadi + user.kullanicisoyadi;
            s.miktar = miktar;
            s.tarih = DateTime.Now; 

            db.logs.Add(s);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult StokCikar(int id, int miktar)
        {
            var stok = db.stoks.FirstOrDefault(a => a.id == id);
            stok.adet = stok.adet - miktar;

            var user = Session["kullaniciadi"] as kullanicilar;
            
            var s = new log();
            s.urunadi = stok.urunler.urunadi;
            s.urunkodu = stok.urunler.urunkodu;
            s.urundegeri = stok.urunler.urundegeri;
            s.urunkilifi = stok.urunler.urunkilifi;
            s.depo = stok.depo1.depoadi;
            s.raf = stok.raf1.rafadi;
            s.girdicikti = 0;
            s.islemyapan = user.kullaniciadi + user.kullanicisoyadi;
            s.miktar = miktar;

            s.tarih = DateTime.Now;

            db.logs.Add(s);

            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Sil(int id)
        {

            var stoksil = db.stoks.Find(id);
            db.stoks.Remove(stoksil);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult StokGetir(int id)    
        {

            List<SelectListItem> depolar = (from d in db.depoes.ToList()
                                            select new SelectListItem
                                            {
                                                Text = d.depoadi,
                                                Value = d.depoid.ToString()
                                            }).ToList();
            List<SelectListItem> raflar = (from r in db.rafs.ToList()
                                           select new SelectListItem
                                           {
                                               Text = r.rafadi,
                                               Value = r.rafid.ToString()
                                           }).ToList();
            ViewBag.raf = raflar;

            ViewBag.depo = depolar;

            var stok = db.stoks.Find(id);
            return View("StokGetir", stok);
        }
        public ActionResult Guncelle(stok p1)
        {

          
            var stok = db.stoks.Find(p1.id);
            stok.urunler.urunadi = p1.urunler.urunadi;
            stok.urunler.urunkodu = p1.urunler.urunkodu;
            stok.urunler.urundegeri = p1.urunler.urundegeri;
            stok.urunler.urunkilifi = p1.urunler.urunkilifi;
            stok.depo = p1.depo;
            stok.raf = p1.raf;
            stok.aciklama = p1.aciklama;
            
            

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void ExporToExcel()
        {
            var degerler = db.stoks.ToList();


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "ARGSTOK";
            ws.Cells["B1"].Value = "STOK RAPORU";


            ws.Cells["A2"].Value = "Rapor Tarihi";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A4"].Value = "STOK ID";
            ws.Cells["B4"].Value = "ÜRÜN ADI";
            ws.Cells["C4"].Value = "ÜRÜN KODU";
            ws.Cells["D4"].Value = "ÜRÜN DEĞERİ";
            ws.Cells["E4"].Value = "ÜRÜN KILIFI";
            ws.Cells["F4"].Value = "DEPO";
            ws.Cells["G4"].Value = "RAF";
            ws.Cells["H4"].Value = "AÇIKLAMA";
            ws.Cells["I4"].Value = "MİKTAR";

            int rowStart = 5;
            foreach (var item in degerler)
            {


                ws.Cells[String.Format("A{0}", rowStart)].Value = item.id;
                ws.Cells[String.Format("B{0}", rowStart)].Value = item.urunler.urunadi;
                ws.Cells[String.Format("C{0}", rowStart)].Value = item.urunler.urunkodu;
                ws.Cells[String.Format("D{0}", rowStart)].Value = item.urunler.urundegeri;
                ws.Cells[String.Format("E{0}", rowStart)].Value = item.urunler.urunkilifi;
                ws.Cells[String.Format("F{0}", rowStart)].Value = item.depo1.depoadi;
                ws.Cells[String.Format("G{0}", rowStart)].Value = item.raf1.rafadi;
                ws.Cells[String.Format("H{0}", rowStart)].Value = item.aciklama;
                ws.Cells[String.Format("I{0}", rowStart)].Value = item.adet;
                rowStart++;

            }

            ws.Cells["A:AZ"].AutoFitColumns();

            Response.Clear();
            
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + "StokRaporu_"+DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")+".xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();


            //Response.Clear();
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.excel";
            //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DummyTTDownload.xls"));

        }
    }
}