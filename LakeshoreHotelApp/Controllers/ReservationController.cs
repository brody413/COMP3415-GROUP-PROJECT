using LakeshoreHotelApp.Data;
using LakeshoreHotelApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LakeshoreHotelApp.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

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
        //GET: CustomerReservationController/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.customer = GetCustomerById(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ViewBag.rooms = _context.Set<Room>().ToList();
            return View("Create");
        }

        //POST: CustomerReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, RoomNumber, BedSize, RoomType, Customer, ReservationStart, ReservationEnd")] Room room)
        {
            //choose a room
            Room dbRoom = ChooseRoom(room.BedSize, room.RoomType);
            dbRoom.Customer = GetCustomerById(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            dbRoom.ReservationStart = room.ReservationStart;
            dbRoom.ReservationEnd = room.ReservationEnd;
            dbRoom.RoomFilled = true;
            //push to database and go tf home
            _context.Update(dbRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");

        }
        public Room ChooseRoom(string bedSize, string roomType)
        {
            foreach (Room room in _context.Set<Room>())
            {
                if (bedSize.Equals(room.BedSize) &&
                roomType.Equals(room.RoomType) &&
                !room.RoomFilled)
                    return room;
            }
            return null;
        }

        public Customer GetCustomerById(string accountId)
        {
            // compare accountId to the Id of each existing customer, return the first one that matches, or null if none match
            foreach (Customer cus in _context.Set<Customer>())
            {
                if (cus.Id.Equals(accountId)) return cus;
            }
            return null;
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
