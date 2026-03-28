using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelMvc.Infrastructure.Models
{
    public class RoomType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
