using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelMvc.Infrastructure.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Location { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
