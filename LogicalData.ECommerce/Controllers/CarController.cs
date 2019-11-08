using LogicalData.Class;
using LogicalData.Repository;
using LogicalData.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogicalData.ECommerce.Controllers
{
    public class CarController : Controller
    {
        CarRepository carRepo = new CarRepository();

        // GET: Car
        public ActionResult Index()
        {
            if (Session["LoginUser"] == null)
                return RedirectToAction("Login", "Account");
            List<DtoCar> listCar = carRepo.GetDtoListCar(Session["LoginUser"].ToString());

            float total = 0;
            foreach (DtoCar c in listCar)
                total += c.SubTotal;

            ViewBag.Total = total;

            return View(listCar);
        }

        // GET: Car/Create
        public ActionResult Create(int id)
        {
            if (Session["LoginUser"] == null)
                return RedirectToAction("Login", "Account");
            ViewBag.NombreArticulo = carRepo.db.Articulos.Find(id).Nombre;
            DtoCar car = new DtoCar() {
                IdArticulo=id,
                Articulo=ViewBag.NombreArticulo
            };
            return View(car);
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(DtoCar carDto)
        {
            // TODO: Add insert logic here
            if (carDto.Cantidad > 0)
            {
                int disponible = carRepo.getArticleCount(carDto.Identificacion, carDto.IdArticulo);
                if (carDto.Cantidad > disponible)
                {
                    ViewBag.MsgCantidad = "La cantidad no debe ser mayor al disponible, disponible:" + disponible;
                    return View();
                }

                Carrito car = carDto.ToCar();
                car.Identificacion = Session["LoginUser"].ToString();

                if (!carRepo.ExistArticle(car.Identificacion, car.IdArticulo))
                {
                    carRepo.Insert(car);
                    carRepo.UpdateArticle(car.Identificacion, car.IdArticulo, car.Cantidad, CarRepository.STATE_CREATE);
                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ViewBag.MsgArticle = "El articulo ya existe en el carrito de compras";
                }
                
            }
            else
            {
                ViewBag.MsgCantidad = "La cantidad debe ser mayor a 0";
            }
            return View();
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["LoginUser"] == null)
                return RedirectToAction("Login", "Account");

            DtoCar dtoCar = carRepo.GetDtoCar(Session["LoginUser"].ToString(), id);
            return View(dtoCar);
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(DtoCar car)
        {
            // TODO: Add update logic here
            if (ModelState.IsValid)
            {
                if (car.Cantidad > 0)
                {
                    int disponible = carRepo.getArticleCount(car.Identificacion, car.IdArticulo);
                    if (car.Cantidad > disponible)
                    {
                        ViewBag.MsgCantidad = "La cantidad no debe ser mayor al disponible, disponible:" + disponible;
                        return View();
                    }
                    carRepo.UpdateArticle(car.Identificacion, car.IdArticulo, car.Cantidad, CarRepository.STATE_EDIT);
                    carRepo.Update(car.ToCar());
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.MsgCantidad = "La cantidad debe ser mayor a 0";
                }

            }
            return View();
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            Carrito car = carRepo.Get(id);
            carRepo.Delete(car);
            carRepo.UpdateArticle(car.Identificacion, car.IdArticulo, car.Cantidad, CarRepository.STATE_DELETE);
            return RedirectToAction("Index");
        }
    }
}
