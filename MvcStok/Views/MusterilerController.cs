using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Views
{
    public class MusterilerController : Controller
    {
        // GET: Musteriler
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult MusteriIndex(string p)
        {
            //var degerler = db.TBLMUSTERILER.ToList();
            //return View(degerler);
            var degerler = from d in db.TBLMUSTERILER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());
        }

        [HttpGet]
        public ActionResult MusteriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MusteriEkle(TBLMUSTERILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("MusteriEkle");
            }
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("MusteriIndex");
        }

        public ActionResult MusteriSil (int id)
        {
            var mstr = db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(mstr);
            db.SaveChanges();
            return RedirectToAction("MusteriIndex");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mstr = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir", mstr);
        }

        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var mstr = db.TBLMUSTERILER.Find(p1.MUSTERIID);
            mstr.MUSTERIAD = p1.MUSTERIAD;
            mstr.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("MusteriIndex");
        }
    }
}