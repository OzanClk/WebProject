using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KutuphaneOtomasyon.Models.Entity;
using Tesseract;
namespace KutuphaneOtomasyon.Controllers
{
    public class BorrowRecordController : Controller
    {
        // GET: BorrowRecord

        LIBRARYDBEntities2 db = new LIBRARYDBEntities2();


        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TakeBook()
        {

            return View();
        }
        [HttpPost]
        public ActionResult TakeBook(TBL_BORROW_BOOK borrow_book)
        {

            var book_state = db.TBL_BOOKLİST.Find(borrow_book.book_name);
            int x = db.TBL_BORROW_BOOK.Count(y => y.user_name == borrow_book.user_name); //Kullanıcı üzerindeki kitap sayısı 3 ten fazla olmamalı
            int count = 0;
            var date_list = db.TBL_BORROW_BOOK.Where(k => k.user_name == borrow_book.user_name);

            foreach (var date in date_list)
            {
                DateTime d1 = DateTime.Parse(date.give_back_date.ToString());
                DateTime d2 = DateTime.Parse(date.date_of_return.ToString());

                TimeSpan d3 = d2 - d1;

                if (d3.TotalDays > 0)
                {
                    ViewBag.GecikmisKitap = "İade tarihi geçmiş kitabınız bulunmaktadır.Lütfen iade ettikten sonra kitap alma işlemini gerçekleştiriniz";
                    return RedirectToAction("IndexUser", "Home");
                }
            }

            if (x > 2)
            {
                ViewBag.Hata1 = "En fazla 3 kitap alabilirsiniz";
                return RedirectToAction("IndexUser", "Home"); //Başka bir sayfaya yönlendir...
            }

            if (book_state.book_state.Equals(true))
            {
                db.TBL_BORROW_BOOK.Add(borrow_book);
                db.SaveChanges();

                return RedirectToAction("IndexUser", "Home");
            }

            


            return View("TakeBook");

        }

        [HttpGet]
        public ActionResult GiveBackBook()
        {

            ViewBag.Result = false;
            ViewBag.Title = "OCR ASP.NET MVC Example";

            ConfirmController a = new ConfirmController();



            return View();

        }

        [HttpPost]
        public ActionResult GiveBackBook(TBL_BOOKLİST book)
        {
            ViewBag.Result = true;



            return View();

        }

        public ActionResult submit(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                ViewBag.Result = true;
                ViewBag.res = "File not Found";
                return View("GiveBackBook");
            }
            using (var engine = new TesseractEngine(Server.MapPath(@"~/tessdata"), "eng", EngineMode.Default))
            {
                using (var image = new System.Drawing.Bitmap(file.InputStream))
                {
                    using (var pix = PixConverter.ToPix(image))
                    {
                        using (var page = engine.Process(pix))
                        {
                            ViewBag.Result = true;
                            ViewBag.res = page.GetText();
                            ViewBag.metin = ISBN_No(ViewBag.res);
                            ViewBag.mean = String.Format("{0:p}", page.GetMeanConfidence());

                        }
                    }
                }
            }

            return View("GiveBackBook");
        }

        public ActionResult MyBooks()
        {

            var user = (string)Session["ID"];
            var id = db.TBL_USER.Where(x => x.user_name == user).Select(k => k.id).FirstOrDefault();
            var mybooks = db.TBL_BORROW_BOOK.Where(x => x.book_name == id).ToList();

            return View(mybooks);

        }

        public ActionResult GiveBackBook2(int id, string book_isbn)
        {

            var isThereBook = db.TBL_BOOKLİST.Where(x => x.book_isbn == book_isbn).FirstOrDefault();

            if (isThereBook != null)
            {

                var book = db.TBL_BORROW_BOOK.Find(id);
                db.TBL_BORROW_BOOK.Remove(book);
                db.SaveChanges();

                return RedirectToAction("IndexUser", "Home");
            }

            return RedirectToAction("IndexUser", "Home");
        }
        public String ISBN_No(String text)
        {

            String[] parcalar = new String[1000];

            parcalar = text.Split(' ');

            for (int i = 0; i < parcalar.Length; i++)
            {
                if (parcalar[i].Contains("ISBN") || parcalar[i].Contains("ISB"))
                {

                    if (parcalar[i + 1].Contains(":"))
                    {
                        return parcalar[i + 2];
                    }
                    else
                    {
                        return parcalar[i + 1];
                    }
                }



            }


            return "";
        }



    }
}