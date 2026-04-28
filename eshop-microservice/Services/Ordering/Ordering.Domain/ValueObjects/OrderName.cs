using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 100;
        public string Value { get; }
        private OrderName(string value) => Value = value;
        public static OrderName Of(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("OrderName cannot be empty.");
            }
            return new OrderName(value);
        }
    }
}
