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
            List<Error> errors = [];

            if (string.IsNullOrWhiteSpace(address) || address.Length > ADDRESS_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("mapAddress", ADDRESS_MAX_LENGTH));

            if (errors.Count > 0)
                return errors;


            var strs = address.Split(", ");


            if (strs.Length < 5 || strs.Length > 6)
                errors.Add(Error.Validation(
                    "MapAddress" + Errors.Validation.ErrorCode,
                    "The address must consist of 5 or 6 lines separated by the string \", \"."));

            if (errors.Count > 0)
                return errors;


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
            List<Error> errors = [];

            if (string.IsNullOrWhiteSpace(country) || country.Length > COUNTRY_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("mapAddress.country", COUNTRY_MAX_LENGTH));


            if (string.IsNullOrWhiteSpace(region) || region.Length > REGION_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("mapAddress.region", REGION_MAX_LENGTH));


            if (string.IsNullOrWhiteSpace(city) || city.Length > CITY_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("mapAddress.city", CITY_MAX_LENGTH));


            if (string.IsNullOrWhiteSpace(street) || street.Length > STREET_MAX_LENGTH)
                errors.Add(Errors.Validation.String.NotBeEmptyAndNotBeLonger("mapAddress.street", STREET_MAX_LENGTH));

            if (houseNumber <= 0)
                errors.Add(Errors.Validation.Int.MustBeGreaterThanZero("mapAddress.houseNumber"));

            if (errors.Count > 0)
                return errors;

            var address = new MapAddress(country, region, city, street, houseNumber, apartmentNumber);

            return address;
        }
    }
}
