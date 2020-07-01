using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneOtomasyon.Models.Entity;

namespace KutuphaneOtomasyon.Controllers
{
    public class WriterController : Controller
    {
        // GET: Writer

        LIBRARYDBEntities2 db = new LIBRARYDBEntities2();

        public ActionResult Index()
        {

            var values = db.TBL_WRITER.ToList();

            return View(values);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {

            return View();

        }
        [HttpPost]
        public ActionResult AddWriter(TBL_WRITER writer)
        {

            db.TBL_WRITER.Add(writer);
            db.SaveChanges();

            return View();

        }
        public ActionResult DeleteWriter(int id)
        {

            var writer = db.TBL_WRITER.Find(id);

            db.TBL_WRITER.Remove(writer);
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult getWriter(int id)
        {
            var writer = db.TBL_WRITER.Find(id);

            return View("getWriter", writer);

        }
        public ActionResult WriterUpdate(TBL_WRITER writer)
        {

            var Tempwriter = db.TBL_WRITER.Find(writer.id);

            Tempwriter.id = writer.id;
            Tempwriter.writer_name = writer.writer_name;

            db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}