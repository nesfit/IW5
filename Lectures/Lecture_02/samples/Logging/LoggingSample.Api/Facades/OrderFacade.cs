using LoggingSample.Api.DAL;
using LoggingSample.Api.Models;

namespace LoggingSample.Api.Facades;

public class OrderFacade
{
    private readonly OrderRepository _orderRepository;
    private readonly ILogger<OrderFacade> _logger;

    public OrderFacade(OrderRepository orderRepository, ILogger<OrderFacade> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public void CreateOrder(CreateOrderModel createModel)
    {
        var entity = new OrderEntity(
            createModel.UserId,
            createModel.OrderId,
            createModel.Name);
        _logger.LogInformation("Creating order {OrderName} ({OrderId}) for {UserId}", entity.Name, entity.OrderId, entity.UserId);
        _orderRepository.AddOrder(entity);
    }

    public IReadOnlyCollection<OrderListModel> GetOrderCreatedByUser(Guid userId)
    {
        _logger.LogInformation("Getting orders for {UserId}", userId);
        return _orderRepository.GetUserOrders(userId)
            .Select(o => new OrderListModel(o.UserId, o.OrderId, o.Name))
            .ToList();
    }
}