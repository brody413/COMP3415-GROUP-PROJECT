using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeshoreHotelApp.Models
{
    public class RestaurantReservation
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public DateTime ReservationTime { get; set; }
    }
}
