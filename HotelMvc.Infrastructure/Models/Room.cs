using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelMvc.Infrastructure.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        public Hotel Hotel { get; set; } = null!;

        [Required]
        public int RoomTypeId { get; set; }

        public RoomType RoomType { get; set; } = null!;

        [Required]
        [Range(1, 10)]
        public int Capacity { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "10000")]
        public decimal PricePerNight { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
