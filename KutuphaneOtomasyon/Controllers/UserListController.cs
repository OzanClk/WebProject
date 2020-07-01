using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneOtomasyon.Models.Entity;

namespace KutuphaneOtomasyon.Controllers
{
    public class UserListController : Controller
    {
        // GET: UserList

        LIBRARYDBEntities2 db = new LIBRARYDBEntities2();

        public ActionResult Index()
        {




            var userList = db.TBL_BORROW_BOOK.ToList();


            return View(userList);
        }
    }
}