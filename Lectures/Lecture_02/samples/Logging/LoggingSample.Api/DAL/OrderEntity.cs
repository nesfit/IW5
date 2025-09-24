namespace LoggingSample.Api.DAL;

public record OrderEntity(
    Guid OrderId,
    Guid UserId,
    string Name);