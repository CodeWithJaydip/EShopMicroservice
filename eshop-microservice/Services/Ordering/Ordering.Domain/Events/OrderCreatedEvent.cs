namespace Ordering.Domain.Events
{
    public record OrderCreatedEvent(OrderId OrderId) : IDomainEvent;
}
