namespace LoggingSample.Api.DAL;

public class OrderRepository
{
    private readonly OrderDatabase _orderDatabase;

    public OrderRepository(OrderDatabase orderDatabase)
    {
        _orderDatabase = orderDatabase;
    }

    public void AddOrder(OrderEntity entity)
    {
        _orderDatabase.Orders.Add(entity);
    }

    public IEnumerable<OrderEntity> GetUserOrders(Guid userId)
    {
        return _orderDatabase.Orders
            .Where(x => x.UserId == userId);
    }
}