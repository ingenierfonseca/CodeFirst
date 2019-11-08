using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicalData.Repository;
using LogicalData.Class;

namespace LogicalData.ECommerce.Controllers
{
    public class ArticleController : Controller
    {
        Repository<Articulo> repo = new Repository<Articulo>();

        // GET: Article
        public ActionResult Index()
        {
            if (Session["LoginUser"] == null)
                return RedirectToAction("Login", "Account");

            return View(repo.GetAll());
        }

        public ActionResult Create()
        {
            if (Session["LoginUser"] == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Articulo article)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(article);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            if (Session["LoginUser"] == null)
                return RedirectToAction("Login", "Account");

            return View(repo.Get(id));
        }

        [HttpPost]
        public ActionResult Edit(Articulo article)
        {
            if (ModelState.IsValid)
            {
                repo.Update(article);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}