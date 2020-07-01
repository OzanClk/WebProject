using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneOtomasyon.Models.Entity;


namespace KutuphaneOtomasyon.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        LIBRARYDBEntities2 db = new LIBRARYDBEntities2();
        public ActionResult Index()
        {

            var values = db.TBL_CATEGORY.ToList();


            return View(values);
        }

        [HttpGet]
        public ActionResult CategoryAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CategoryAdd(TBL_CATEGORY category)
        {

            db.TBL_CATEGORY.Add(category);
            db.SaveChanges();

            return View();

        }
        public ActionResult CategoryDelete(int id)
        {
            var category = db.TBL_CATEGORY.Find(id);
            db.TBL_CATEGORY.Remove(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult getCategory(int id)
        {

            var category = db.TBL_CATEGORY.Find(id);

            return View("getCategory", category);

        }

        public ActionResult CategoryUpdate(TBL_CATEGORY category)
        {

            var tempCategory = db.TBL_CATEGORY.Find(category.id);

            tempCategory.ad = category.ad;
            tempCategory.id = category.id;

            db.SaveChanges();

            return RedirectToAction("Index");


        }
       


    }
}