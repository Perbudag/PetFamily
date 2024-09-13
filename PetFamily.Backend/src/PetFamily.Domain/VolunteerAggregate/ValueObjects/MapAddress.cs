using PetFamily.Domain.Shared.Models;

namespace PetFamily.Domain.VolunteerAggregate.ValueObjects
{
    public record MapAddress
    {
        public const int COUNTRY_MAX_LENGTH = 100;
        public const int REGION_MAX_LENGTH = 100;
        public const int CITY_MAX_LENGTH = 100;
        public const int STREET_MAX_LENGTH = 50;
        public const int ADDRESS_MAX_LENGTH = COUNTRY_MAX_LENGTH + REGION_MAX_LENGTH + CITY_MAX_LENGTH + STREET_MAX_LENGTH + 20;

        public string Country { get; }
        public string Region { get; }
        public string City { get; }
        public string Street { get; }
        public int HouseNumber { get; }
        public int? ApartmentNumber { get; }

        private MapAddress(string country, string region, string city, string street, int houseNumber, int? apartmentNumber)
        {
            Country = country;
            Region = region;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
        }

        public override string ToString()
        {
            string address = $"{Country}, {Region}, {City}, {Street}, {HouseNumber}";

            if (ApartmentNumber != null)
                address += ", " + ApartmentNumber;

            return address;
        }

        public static Result<MapAddress> Parse(string address)
        {
            if (string.IsNullOrWhiteSpace(address) || address.Length > ADDRESS_MAX_LENGTH)
                return Error.Validation("MapAddress.Parse.Invalid", $"The \"address\" argument must not be empty and must consist of no more than {ADDRESS_MAX_LENGTH} characters.");

            var strs = address.Split(", ");

            if (strs.Length < 5 || strs.Length > 6)
                return Error.Validation("MapAddress.Parse.Invalid", "The \"address\" argument must consist of 5 or 6 elements separated by the string \", \".");

            string country = strs[1];
            string region = strs[2];
            string city = strs[3];
            string street = strs[4];
            int houseNumber = int.Parse(strs[5]);
            int? apartmentNumber = null;

            if (strs.Length == 6)
                apartmentNumber = int.Parse(strs[6]);

            return Create(country, region, city, street, houseNumber, apartmentNumber);
        }

        public static Result<MapAddress> Create(string country, string region, string city, string street, int houseNumber, int? apartmentNumber)
        {
            if (string.IsNullOrWhiteSpace(country) || country.Length > ADDRESS_MAX_LENGTH)
                return Error.Validation("MapAddress.Create.Invalid", $"The \"country\" argument must not be empty and must consist of no more than {COUNTRY_MAX_LENGTH} characters.");

            if (string.IsNullOrWhiteSpace(region) || region.Length > ADDRESS_MAX_LENGTH)
                return Error.Validation("MapAddress.Create.Invalid", $"The \"region\" argument must not be empty and must consist of no more than {REGION_MAX_LENGTH} characters.");

            if (string.IsNullOrWhiteSpace(city) || city.Length > ADDRESS_MAX_LENGTH)
                return Error.Validation("MapAddress.Create.Invalid", $"The \"city\" argument must not be empty and must consist of no more than {CITY_MAX_LENGTH} characters.");

            if (string.IsNullOrWhiteSpace(street) || street.Length > ADDRESS_MAX_LENGTH)
                return Error.Validation("MapAddress.Create.Invalid", $"The \"street\" argument must not be empty and must consist of no more than {STREET_MAX_LENGTH} characters.");

            var address = new MapAddress(country, region, city, street, houseNumber, apartmentNumber);

            return address;
        }
    }
}
