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
        public string id { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public bool FacesLake { get; set; }
        [Required]
        public bool IsSuite { get; set; }
        [Required]
        public bool RoomFilled { get; set; }
        [Required]
        public Customer Customer { get; set; }


    }
}
