namespace LoggingSample.Api.Models;

public record CreateOrderModel(
    Guid UserId,
    Guid OrderId,
    string Name);