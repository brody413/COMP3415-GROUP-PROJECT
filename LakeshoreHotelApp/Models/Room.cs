using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeshoreHotelApp.Models
{
    public class Room
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public bool RoomFilled { get; set; }
        [Required]
        public string BedSize { get; set; }
        [Required]
        public string RoomType { get; set; }
        //reference to reserving customer
        public Customer? Customer { get; set; }
        public string customerID { get; set; }
        public DateTime? ReservationStart { get; set; }
        public DateTime? ReservationEnd { get; set; }

    }
}
