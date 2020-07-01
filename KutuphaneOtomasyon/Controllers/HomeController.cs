using KutuphaneOtomasyon.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KutuphaneOtomasyon.Controllers
{
    
    public class HomeController : Controller
    {

        LIBRARYDBEntities2 db = new LIBRARYDBEntities2();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult IndexUser()
        {

            var user = (string)Session["ID"];
           // var id = db.TBL_BORROW_BOOK.Where(x => x.user_name.ToString() == user).Select(k => k.id).FirstOrDefault();
            var mybooks = db.TBL_BORROW_BOOK.Where(x => x.user_name.ToString() == user).ToList();

    
            return View(mybooks);
        }


    }
}