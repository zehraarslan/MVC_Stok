using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Views
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult SatisIndex()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SatisEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SatisEkle(TBLSATISLAR p1)
        {
            db.TBLSATISLAR.Add(p1);
            db.SaveChanges();
            return RedirectToAction("SatisIndex");
        }
    }
}