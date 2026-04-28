namespace Ordering.Domain.Events
{
   public record OrderUpdatedEvent(OrderId OrderId) : IDomainEvent; 
}
