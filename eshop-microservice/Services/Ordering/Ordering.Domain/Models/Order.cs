using Ordering.Domain.Events;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; private set; } = default!;
        public OrderName OrderName { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice => _orderItems.Sum(x => x.Price * x.Quantity);

        public static Order Create(OrderId orderId, CustomerId customerId, OrderName orderName, Address billingAddress, Address shippingAddress, Payment payment)
        {
            var order = new Order
            {
                Id = orderId,
                CustomerId = customerId,
                OrderName = orderName,
                BillingAddress = billingAddress,
                ShippingAddress = shippingAddress,
                Payment = payment
            };
            order.AddDomainEvent(new OrderCreatedEvent(order.Id));
            return order;
        }

        public void Update(OrderId orderId, CustomerId customerId, OrderName orderName, Address billingAddress, Address shippingAddress, Payment payment)
        {
            Id = orderId;
            CustomerId = customerId;
            OrderName = orderName;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            Payment = payment;
            AddDomainEvent(new OrderUpdatedEvent(orderId));
        }

        public void AddOrderItem(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id, productId, quantity, price);
            _orderItems.Add(orderItem);
        }

        public void RemoveOrderItem(ProductId productId)
        {
            var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem is not null)
            {
                _orderItems.Remove(orderItem);
            }
        }
    }
}
