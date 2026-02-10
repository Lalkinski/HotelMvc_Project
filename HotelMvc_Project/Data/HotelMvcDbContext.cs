using HotelMvc_Project.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc_Project.Data
{
    public class HotelMvcDbContext : DbContext
    {
        public HotelMvcDbContext(DbContextOptions<HotelMvcDbContext> options)
           : base(options) { }

        public DbSet<Guest> Guests { get; set; } = null!;
        public DbSet<HotelRoom> Rooms { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HotelRoom>()
                .HasIndex(hr => hr.RoomNumber)
                .IsUnique();

            modelBuilder.Entity<Guest>()
                .HasIndex(g => g.Email)
                .IsUnique();

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Guest)
                .WithMany(g => g.Reservations)
                .HasForeignKey(r => r.GuestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(hr => hr.HotelRoom)
                .WithMany(hr => hr.Reservations)
                .HasForeignKey(r => r.HotelRoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
