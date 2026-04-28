namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? EmailAddress { get; set; } = default!;
        public string AddressLine { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;     
        public string ZipCode { get; set; } = default!;

        protected Address() { }

        private Address(string firstName, string lastName, string emailAddress, string addressLine, string city, string state, string country, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string city, string state, string country, string zipCode)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(addressLine);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(city);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(state);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(country);
            ArgumentNullException.ThrowIfNullOrWhiteSpace(zipCode);
            return new Address(firstName, lastName, emailAddress, addressLine, city, state, country, zipCode);
        }
    }
}
