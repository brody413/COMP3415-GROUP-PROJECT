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
    public class EmployeeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerEmployeeController
        [Authorize(Roles ="Employee")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerEmployeeController/Details
        [Authorize(Roles = "Employee")]
        public ActionResult Details()
        {
            List<Room> rooms = _context.rooms.Where(r => r.Customer != null && r.RoomFilled).ToList();
            return View("Details", rooms);
        }
        //GET: CustomerEmployeeController/Create
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult Create()
        {
            ViewBag.customers = _context.Set<Customer>().ToList();
            ViewBag.rooms = _context.Set<Room>().ToList();
            return View("Create");
        }

        //POST: CustomerEmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
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

        // GET: CustomerEmployeeController/Edit/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(string id)
        {
            Room room = await _context.FindAsync<Room>(id);
            ViewBag.customer = GetCustomerById(room.customerID);
            return View(room);
        }

        // POST: CustomerEmployeeController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> EditConfirmed(Room room)
        {
            //delete
            string RoomType = "", BedSize = "";
            DateTime ReservationStart = new DateTime(), ReservationEnd = new DateTime();
            if (room != null)
            {
                RoomType = room.RoomType;
                BedSize = room.BedSize;
                ReservationStart = (DateTime)room.ReservationStart;
                ReservationEnd = (DateTime)room.ReservationEnd;
                room = await _context.rooms.FindAsync(room.Id);
                room.RoomFilled = false;
                room.customerID = null;
                room.ReservationStart = room.ReservationEnd = null;
                _context.Update(room);
                await _context.SaveChangesAsync();
            }

            return await Create(new Room { RoomType = RoomType, BedSize = BedSize, ReservationStart = ReservationStart, ReservationEnd = ReservationEnd, customerID = User.FindFirst(ClaimTypes.NameIdentifier).Value, Customer = GetCustomerById(User.FindFirst(ClaimTypes.NameIdentifier).Value) });
        }

        // GET: CustomerEmployeeController/Delete0
        [Authorize(Roles = "Employee")]
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
