using BuberBreakfast.Models;
using BuberBreakfast.Persistance;
using BuberBreakfast.ServiceErrors;
using ErrorOr;

namespace BuberBreakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private readonly BuberBreakfastDbContext dbContext;

    public BreakfastService(BuberBreakfastDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    private static readonly Dictionary<Guid, Breakfast> _breakfasts = new();

    public ErrorOr<Created> CreateBreakfast(Breakfast breakfast)
    {
        dbContext.Breakfasts.Add(breakfast);
        dbContext.SaveChanges();

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        var breakfast = dbContext.Breakfasts.Find(id);
        
        if (breakfast is null){
            return Errors.Breakfast.NotFound;
        }

        dbContext.Remove(breakfast);
        dbContext.SaveChanges();

        return Result.Deleted;
    }

    public ErrorOr<Breakfast> GetBreakfast(Guid id)
    {
        var breakfast = dbContext.Breakfasts.Find(id);
        
        if (breakfast is not null)
        {
            return breakfast;
        }
        
        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(Breakfast breakfast)
    {
        var isNewlyCreated = dbContext.Breakfasts.Find(breakfast.Id) is not Breakfast dbbreakfast;
        
        if(isNewlyCreated)
        {
            dbContext.Breakfasts.Add(breakfast);
        }
        else
        {
            dbContext.Breakfasts.Update(breakfast);
        }

        dbContext.SaveChanges();

        return new UpsertedBreakfast(isNewlyCreated);
    }
}
