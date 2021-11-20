using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakeshoreHotelApp.Controllers
{
    public class ReservationController : Controller
    {
        // GET: CustomerReservationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerReservationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerReservationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerReservationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerReservationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerReservationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
