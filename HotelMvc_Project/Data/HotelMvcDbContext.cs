using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc_Project.Data
{
    public class HotelMvcDbContext(DbContextOptions<HotelMvcDbContext> options) : IdentityDbContext(options)
    {
    }
}
