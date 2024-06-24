using HotelStaffPortal.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelStaffPortal.Web.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options): base (options)
        {
            
        }
        public DbSet<Staff> AllStaff { get; set; }   
    }
}
