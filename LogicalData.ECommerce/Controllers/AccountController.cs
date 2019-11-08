using LogicalData.Class;
using LogicalData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogicalData.ECommerce.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository repo = new AccountRepository();

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario user)
        {
            var u = repo.ValidateUser(user.Email, user.Contrasenya);
            if (u != null)
            {
                Session["LoginUser"] = u.Identificacion;
                return RedirectToAction("Index", "Article", new { r = Request.Url.ToString() });
            }
            else
            {
                ModelState.AddModelError("", "El usuario on contrasenya es incorrecto");
            }
            
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuario user)
        {
            if (ModelState.IsValid)
            {
                var result = repo.Register(user);
                if (result.Equals("Exito"))
                    return Redirect("Login");
                else
                    ViewBag.Msg = result;
            }

            return View();
        }

        public ActionResult Edit()
        {
            if (Session["LoginUser"] == null)
                return RedirectToAction("Login");

            var user = repo.Get(Session["LoginUser"].ToString());

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(Usuario user)
        {
            if (Session["LoginUser"] == null)
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                repo.Update(user);
                TempData["msg"] = "success";
            }

            return View();
        }
    }
}