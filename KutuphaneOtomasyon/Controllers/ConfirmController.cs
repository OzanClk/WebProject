using KutuphaneOtomasyon.Models.Entity;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KutuphaneOtomasyon.Controllers
{
    public class ConfirmController : Controller
    {
        // GET: Confirm

        public int user_id;

        LIBRARYDBEntities2 db = new LIBRARYDBEntities2();
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(TBL_USER user)
        {

            user_id = user.id;
            String user_name = user.user_name;

            var user_type = db.TBL_USER.Find(user.id);

            String k = user_type.user_type;
            k = k.Trim();

            var kullanıcı = db.TBL_USER.FirstOrDefault(x => x.id == user.id && x.password == user.password);
            var kullanıcı_kitapları = db.TBL_BORROW_BOOK.FirstOrDefault(x => x.id == user_type.id);



            if (kullanıcı != null)
            {



                if (k == "admin")
                {
                    FormsAuthentication.SetAuthCookie(kullanıcı.user_name, false);

                    /*TempData["id"] = kullanıcı_kitapları.id.ToString();
                    TempData["Book_Name"] = db.TBL_BOOKLİST.Where(x => x.id == user_type.id).ToString();
                    TempData["Borrow_Date"] = kullanıcı_kitapları.borrow_date.ToString();
                    TempData["Give_Back"] = kullanıcı_kitapları.give_back_date.ToString();*/

                    return RedirectToAction("Index", "Home");
                }
                if (k == "user")
                {
                    FormsAuthentication.SetAuthCookie(kullanıcı.user_name, false);

                    Session["ID"] = kullanıcı.id.ToString();
                   
                    return RedirectToAction("IndexUser", "Home");
                }


            }

            return View();
        }




    }
}