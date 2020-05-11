using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArgStok.Models.Entity;
using OfficeOpenXml;

namespace ArgStok.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        argdbstoksEntities db = new argdbstoksEntities();
        [Components.SessionAuthorization]
        public ActionResult Index()
        {
            var degerler = db.logs.ToList();
            return View(degerler);
            //IList<log> emplist = db.logs.Select(x => new log
            //{
            //    id = x.id,
            //    urunkodu = x.urunkodu,
            //    urunadi = x.urunadi,
            //    girdicikti = x.girdicikti,
            //    islemyapan = x.islemyapan,
            //    miktar = x.miktar,
            //    tarih = x.tarih
            //}).ToList();
            //return View(emplist); 
        }

        public void ExporToExcel()
        {
            var degerler = db.logs.ToList();


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "ARGSTOK";
            ws.Cells["B1"].Value = "Girdi/Çıktı Raporları";

          
            ws.Cells["A2"].Value = "Rapor Tarihi";
            ws.Cells["B2"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A4"].Value = "ID";
            ws.Cells["B4"].Value = "ÜRÜN ADI";
            ws.Cells["C4"].Value = "ÜRÜN KODU";
            ws.Cells["D4"].Value = "ÜRÜN DEĞERİ";
            ws.Cells["E4"].Value = "ÜRÜN KILIFI";
            ws.Cells["F4"].Value = "DEPO";
            ws.Cells["G4"].Value = "RAF";
            ws.Cells["H4"].Value = "İŞLEM YAPAN";
            ws.Cells["I4"].Value = "GİRDİ/ÇIKTI";
            ws.Cells["J4"].Value = "MİKTAR";
            ws.Cells["K4"].Value = "TARİH";

            int rowStart = 5;
            foreach (var item in degerler)
            {
                

                ws.Cells[String.Format("A{0}", rowStart)].Value = item.id;
                ws.Cells[String.Format("B{0}", rowStart)].Value = item.urunadi;
                ws.Cells[String.Format("C{0}", rowStart)].Value = item.urunkodu;
                ws.Cells[String.Format("D{0}", rowStart)].Value = item.urundegeri;
                ws.Cells[String.Format("E{0}", rowStart)].Value = item.urunkilifi;
                ws.Cells[String.Format("F{0}", rowStart)].Value = item.depo;
                ws.Cells[String.Format("G{0}", rowStart)].Value = item.raf;
                ws.Cells[String.Format("H{0}", rowStart)].Value = item.islemyapan;
                if (item.girdicikti == 0)
                {
                    
                    
                    ws.Cells[String.Format("I{0}", rowStart)].Value = "Stok Çıkışı";

                }
                else
                {
                    
                    ws.Cells[String.Format("I{0}", rowStart)].Value = "Stok Girişi";
                }
                ws.Cells[String.Format("J{0}", rowStart)].Value = item.miktar;
                ws.Cells[String.Format("K{0}", rowStart)].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", item.tarih);
                rowStart++;

            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + "StokHareketleri_" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
    }

}