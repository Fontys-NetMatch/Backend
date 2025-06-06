using TravelPlanner.Domain.Interfaces.BLL.MockGeneration;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Entities.Product;
using TravelPlanner.Domain.New_Models.Enums;
using TravelPlanner.Domain.New_Models.Enums.Enums;

public class ProductInformationFactory : IProductInformationFactory
{
    private readonly Random _random = new();

    public ProductInformation GenerateRandom()
    {
        var type = GetRandomEnum<ProductType>();
        var codes = new List<ProductCode>
        {
            new ProductCode(GetRandomEnum<TransportTypeValue>()),
            new ProductCode(GetRandomEnum<TransportTypeValue>())
        };

        var contentSupplier = GetRandomEnum<ContentSupplier>();

        var location = new HotelLocationAndContactInformation(
            countryCode: GetRandomString(2),
            countryName: GetRandomString(8),
            region: GetRandomString(6),
            destination: GetRandomString(6),
            city: GetRandomString(6),
            address: $"{_random.Next(1, 100)} {GetRandomString(10)} St.",
            telephoneNumber: $"+{_random.Next(1, 100)}-{_random.Next(1000000, 9999999)}",
            geoCoordinates: new GeoCoordinates(
                longitude: GetRandomDouble(-180, 180),
                latitude: GetRandomDouble(-90, 90)
            ),
            phoneNumber: $"+{_random.Next(1, 100)}-{_random.Next(1000000, 9999999)}"
        );

        MediaType mediatype = (MediaType)_random.Next(1, 2); 
        MediaSource mediasoure = (MediaSource)_random.Next(1, 2);
        var media = new MediaContext(
            id: _random.Next(1,10000),
            identifier: Guid.NewGuid().ToString(),
            description: GetRandomString(15),
            title: GetRandomString(10),
            category: GetRandomString(8),
            mediaType: mediatype,
            mediaSource: mediasoure
        );

        var errata = new Errata(
            title: GetRandomString(10),
            content: GetRandomString(20),
            startDate: DateTime.Now.AddDays(-_random.Next(1, 100)),
            endDate: DateTime.Now.AddDays(_random.Next(1, 100))
        );

        var (checkInFrom, checkInUntil, checkOut) = GenerateLogicalTimes();

        var characteristics = new ProductCharacteristics(
            numberOfBedrooms: _random.Next(1, 5),
            numberOfBathrooms: _random.Next(1, 3),
            size: GetRandomDouble(20, 200),
            checkInFrom: checkInFrom,
            checkInUntil: checkInUntil,
            checkOut: checkOut,
            numberOfPets: _random.Next(0, 3),
            eventsAllowed: _random.Next(0, 2) == 1,
            minimumAge: _random.Next(18, 25),
            maxBabyBeds: _random.Next(0, 2)
        );

        var distances = new List<DistanceInformation>
        {
            new DistanceInformation("Beach", "Main Beach", GetRandomDouble(0.1, 5), "Near the resort"),
            new DistanceInformation("Airport", "International Airport", GetRandomDouble(5, 50), "Transfer available")
        };

        return new ProductInformation(type, codes, contentSupplier, location, media, errata, characteristics, distances);
    }

    private T GetRandomEnum<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(_random.Next(values.Length))!;
    }

    private string GetRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var result = new char[length];
        for (int i = 0; i < length; i++)
            result[i] = chars[_random.Next(chars.Length)];
        return new string(result);
    }

    private double GetRandomDouble(double min, double max)
    {
        return _random.NextDouble() * (max - min) + min;
    }

    private (string checkInFrom, string checkInUntil, string checkOut) GenerateLogicalTimes()
    {
        // Generate a check-in time between 12:00 and 16:00
        int checkInFromHour = _random.Next(12, 15);
        int checkInFromMinute = _random.Next(0, 2) * 30; // 0 or 30
        var checkInFrom = $"{checkInFromHour:D2}:{checkInFromMinute:D2}";

        // check-in until 1–3 hours later
        int checkInUntilHour = checkInFromHour + _random.Next(1, 3);
        int checkInUntilMinute = _random.Next(0, 2) * 30;
        var checkInUntil = $"{checkInUntilHour:D2}:{checkInUntilMinute:D2}";

        // check-out between 9:00 and 12:00
        int checkOutHour = _random.Next(9, 12);
        int checkOutMinute = _random.Next(0, 2) * 30;
        var checkOut = $"{checkOutHour:D2}:{checkOutMinute:D2}";

        return (checkInFrom, checkInUntil, checkOut);
    }
}
