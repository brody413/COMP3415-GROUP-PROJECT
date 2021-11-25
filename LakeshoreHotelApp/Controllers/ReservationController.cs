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
using Microsoft.EntityFrameworkCore;

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

        // GET: CustomerReservationController/Details
        public ActionResult Details()
        {
            string accountID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Room> rooms = _context.rooms.Where(r => r.Customer != null && r.Customer.Id.Equals(accountID)).ToList();
            return View("Details", rooms);
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
            //push to database and go home
            _context.Update(dbRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));

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
        public async Task<IActionResult> Edit(string id)
        {
            Room room = await _context.FindAsync<Room>(id);
            return View(room);
        }

        // POST: CustomerReservationController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(string id)
        {
            //delete
            Room room = await _context.rooms.FindAsync(id);
            string RoomType = "", BedSize = "";
            if (room != null)
            {
                RoomType = room.RoomType;
                BedSize = room.BedSize;
                room.RoomFilled = false;
                room.customerID = null;
                room.ReservationStart = room.ReservationEnd = null;
                _context.Update(room);
                await _context.SaveChangesAsync();
            }

            return await Create(new Room { RoomType = RoomType, BedSize = BedSize});
        }

        // GET: CustomerReservationController/Delete
        public async Task<ActionResult> Delete(string id)
        {
            Room room = await _context.rooms.FindAsync(id);
            if (room != null)
            {
                room.RoomFilled = false;
                room.customerID = null;
                room.ReservationStart = room.ReservationEnd = null;
                _context.Update(room);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details));
        }
    }
}
