using BuberBreakfast.Models;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Persistance;

public class BuberBreakfastDbContext : DbContext
{
    //DbContext is our database which is our models and migrations 
    public BuberBreakfastDbContext(DbContextOptions<BuberBreakfastDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Breakfast> Breakfasts { get; set; }


}
