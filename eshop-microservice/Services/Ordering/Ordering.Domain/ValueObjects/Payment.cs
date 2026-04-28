namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get; set; } = default!;
        public string? CardName { get; set; } = default!;
        public DateTime Expiration { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!;
    }
}
