using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LakeshoreHotelApp.Data;
using LakeshoreHotelApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LakeshoreHotelApp.Controllers
{
    public class RestaurantReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RestaurantReservations
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Employee"))
            {
                var reservations = _context.RestaurantReservations.Include(r => r.Customer);
                return View(await reservations.ToListAsync());
            }
            else
            {
                var reservations = _context.RestaurantReservations.Include(r => r.Customer).Where(r => r.Customer.Id == User.FindFirst(ClaimTypes.NameIdentifier).Value);
                return View(await reservations.ToListAsync());
            }
        }

        // GET: RestaurantReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantReservation = await _context.RestaurantReservations
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantReservation == null)
            {
                return NotFound();
            }

            return View(restaurantReservation);
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

        // GET: RestaurantReservations/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.customers, "Id", "Id");
            if (User.IsInRole("Employee"))
            {
                ViewData["Customers"] = new SelectList(_context.customers, "Id", "FirstName");
            } else if (User.Identity.IsAuthenticated)
            {
                ViewData["Customer"] = GetCustomerById(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            return View();
        }

        // POST: RestaurantReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(RestaurantReservation restaurantReservation)
        {
            if (User.IsInRole("Employee")) {
                var Customer = GetCustomerById(restaurantReservation.CustomerId);
                restaurantReservation.Customer = Customer;
            } else if (User.Identity.IsAuthenticated)
            {
                var Customer = GetCustomerById(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                restaurantReservation.Customer = Customer;
                restaurantReservation.CustomerId = Customer.Id;
            }
                _context.Add(restaurantReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: RestaurantReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantReservation = await _context.RestaurantReservations.FindAsync(id);
            if (restaurantReservation == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.customers, "Id", "Id", restaurantReservation.CustomerId);
            ViewBag.CustomerName = GetCustomerById(restaurantReservation.CustomerId).FirstName;
            return View(restaurantReservation);
        }

        // POST: RestaurantReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RestaurantReservation restaurantReservation)
        {
            if (id != restaurantReservation.Id)
            {
                return NotFound();
            }

            Customer customer = GetCustomerById(restaurantReservation.CustomerId);
            restaurantReservation.Customer = customer;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurantReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantReservationExists(restaurantReservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.customers, "Id", "Id", restaurantReservation.CustomerId);
            return View(restaurantReservation);
        }

        // GET: RestaurantReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantReservation = await _context.RestaurantReservations
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurantReservation == null)
            {
                return NotFound();
            }

            return View(restaurantReservation);
        }

        // POST: RestaurantReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurantReservation = await _context.RestaurantReservations.FindAsync(id);
            _context.RestaurantReservations.Remove(restaurantReservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantReservationExists(int id)
        {
            return _context.RestaurantReservations.Any(e => e.Id == id);
        }
    }
}
