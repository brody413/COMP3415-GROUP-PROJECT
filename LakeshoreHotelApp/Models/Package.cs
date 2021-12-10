using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeshoreHotelApp.Models
{
    public class Package
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set;  }

        [Required]
        public int TypeId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Range(0.01, 99999)]
        public double Price { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string Description { get; set; }

        public string Picture { get; set; }

    }
}
