using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Views
{
    public class UrunlerController : Controller
    {
        // GET: Urunler
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult UrunIndex(int sayfa=1)
        {
            //var degerler = db.TBLURUNLER.ToList();
            var degerler = db.TBLURUNLER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> deger = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr=deger;
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("UrunIndex");
        }

        public ActionResult UrunSil(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("UrunIndex");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> deger = (from i in db.TBLKATEGORILER.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.KATEGORIAD,
                                              Value = i.KATEGORIID.ToString()
                                          }).ToList();
            ViewBag.dgr = deger;
            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urun = db.TBLURUNLER.Find(p1.URUNID);
            urun.URUNAD = p1.URUNAD;
            urun.MARKA = p1.MARKA;
            urun.FIYAT = p1.FIYAT;
            urun.STOK = p1.STOK;
            //urun.URUNKATEGORI = p1.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("UrunIndex");
        }
    }
}