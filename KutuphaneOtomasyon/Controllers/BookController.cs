using KutuphaneOtomasyon.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Tesseract;
namespace KutuphaneOtomasyon.Controllers
{


    public class BookController : Controller
    {
        // GET: Book


        public ActionResult Index(string book_info)
        {

            var books = from book in db.TBL_BOOKLİST select book;

            if (!string.IsNullOrEmpty(book_info))
            {
                books = books.Where(m => m.book_isbn.Contains(book_info));

            }

            return View(books.ToList());
        }
        public ActionResult Index2(string book_info)
        {

            var books = from book in db.TBL_BOOKLİST select book;

            if (!string.IsNullOrEmpty(book_info))
            {
                books = books.Where(m => m.book_isbn.Contains(book_info));

            }

            return View(books.ToList());
        }

        public ActionResult BookDelete(int id)
        {
            var book = db.TBL_BOOKLİST.Find(id);
            db.TBL_BOOKLİST.Remove(book);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult BookUpdate(TBL_BOOKLİST book)
        {

            var TempBook = db.TBL_BOOKLİST.Find(book.id);

            TempBook.book_name = book.book_name;
            TempBook.book_isbn = book.book_isbn;
            TempBook.number_of_pages = book.number_of_pages;

            db.SaveChanges();

            return RedirectToAction("Index");


        }


        public ActionResult getBook(int id)
        {

            var book = db.TBL_BOOKLİST.Find(id);

            return View("getBook", book);


        }


        LIBRARYDBEntities2 db = new LIBRARYDBEntities2();



        [HttpGet]
        public ActionResult AddBook2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBook2(TBL_BOOKLİST book)
        {



            db.TBL_BOOKLİST.Add(book);
            db.SaveChanges();

            return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult AddBook()
        {

            ViewBag.Result = false;
            ViewBag.Title = "OCR ASP.NET MVC Example";
            return View();

        }

        [HttpPost]
        public ActionResult AddBook(TBL_BOOKLİST book)
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
                return View("AddBook");
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

            return View("AddBook");
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