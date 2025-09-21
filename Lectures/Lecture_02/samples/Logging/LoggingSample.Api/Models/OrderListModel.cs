namespace LoggingSample.Api.Models;

public record OrderListModel(
    Guid UserId,
    Guid OrderId,
    string Name);