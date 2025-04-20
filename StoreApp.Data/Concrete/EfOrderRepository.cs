using System.Threading.Tasks;
using StoreApp.Data.Abstract;

namespace StoreApp.Data.Concrete;

public class EfOrderRepository : IOrderRepository
{
    private readonly StoreDbContext _context;
    public EfOrderRepository(StoreDbContext context)
    {
        _context = context;
    }
    public IQueryable<Order> Orders => _context.Orders;

    public async Task SaveOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }
}