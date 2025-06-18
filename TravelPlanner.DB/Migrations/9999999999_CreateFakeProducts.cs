using System.Text;
using LinqToDB;
using Newtonsoft.Json;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.GenerationModels;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.DB.Migrations;

[DevOnly]
public class CreateFakeProducts : IMigration
{


    public int ProductCount = 1000;
    public int MinProductsPerLocationAndType = 5;
    
    public List<string> locations = [
        "Amsterdam, Netherlands",
        "Paris, France",
        "Tokyo, Japan",
        "New York, USA",
        "Sydney, Australia",
        "London, United Kingdom",
        "Rome, Italy",
        "Bangkok, Thailand",
        "Cape Town, South Africa",
        "Rio de Janeiro, Brazil",
        "Barcelona, Spain",
        "Dubai, United Arab Emirates",
        "Toronto, Canada",
        "Singapore, Singapore",
        "Marrakech, Morocco",
        "San Francisco, USA",
        "Seoul, South Korea",
        "Istanbul, Turkey",
        "Mexico City, Mexico",
        "Auckland, New Zealand",
        "Vienna, Austria",
        "Hong Kong, China",
        "Havana, Cuba",
        "Stockholm, Sweden",
        "Cairo, Egypt",
        "Vancouver, Canada",
        "Kyoto, Japan",
        "Buenos Aires, Argentina",
        "Athens, Greece",
        "Prague, Czech Republic",
        "Mumbai, India",
        "Lisbon, Portugal",
        "Reykjavik, Iceland",
        "Nairobi, Kenya",
        "Edinburgh, United Kingdom",
        "Santiago, Chile",
        "Hanoi, Vietnam",
        "Budapest, Hungary",
        "Chicago, USA",
        "Lima, Peru"
    ];
    public List<FakeProductType> Types = [
    new FakeProductType
    {
        Value = "flight",
        NameEn = "Flight from {start} to {end}",
        NameNl = "Vlucht van {start} naar {end}",
        DescriptionEn = "A one-way or round-trip airline ticket from {start} to {end}, available in economy, business, or first-class.",
        DescriptionNl = "Een enkele reis of retour vliegticket van {start} naar {end}, beschikbaar in economy, business of eerste klasse."
    },
    new FakeProductType
    {
        Value = "hotel",
        NameEn = "Hotel in {end}",
        NameNl = "Hotel in {end}",
        DescriptionEn = "A hotel room in {end} with options for single, double, or suite accommodations, including amenities like Wi-Fi and breakfast.",
        DescriptionNl = "Een hotelkamer in {end} met opties voor eenpersoons-, tweepersoons- of suitekamers, inclusief voorzieningen zoals Wi-Fi en ontbijt."
    },
    new FakeProductType
    {
        Value = "car",
        NameEn = "Car Rental in {end}",
        NameNl = "Autohuur in {end}",
        DescriptionEn = "A rental car in {end}, available in compact, SUV, or luxury models for flexible travel.",
        DescriptionNl = "Een huurauto in {end}, beschikbaar in compacte, SUV- of luxemodellen voor flexibel reizen."
    },
    new FakeProductType
    {
        Value = "cruise",
        NameEn = "Cruise from {start} to {end}",
        NameNl = "Cruise van {start} naar {end}",
        DescriptionEn = "A multi-day cruise package from {start} to {end}, including cabin accommodations, dining, and onboard activities.",
        DescriptionNl = "Een meerdaags cruisepakket van {start} naar {end}, inclusief cabine-accommodaties, dineren en activiteiten aan boord."
    },
    new FakeProductType
    {
        Value = "train",
        NameEn = "Train from {start} to {end}",
        NameNl = "Trein van {start} naar {end}",
        DescriptionEn = "A train ticket from {start} to {end}, offering standard or first-class seating with scenic views.",
        DescriptionNl = "Een treinticket van {start} naar {end}, met standaard- of eersteklas zitplaatsen en schilderachtige uitzichten."
    },
    new FakeProductType
    {
        Value = "tour",
        NameEn = "Guided Tour in {end}",
        NameNl = "Rondleiding in {end}",
        DescriptionEn = "A guided tour package in {end}, featuring expert-led visits to key attractions and cultural sites.",
        DescriptionNl = "Een rondleidingspakket in {end}, met deskundige bezoeken aan belangrijke bezienswaardigheden en culturele locaties."
    },
    new FakeProductType
    {
        Value = "camping",
        NameEn = "Camping in {end}",
        NameNl = "Kamperen in {end}",
        DescriptionEn = "A camping spot in {end}, equipped for tents or RVs with access to nature and basic facilities.",
        DescriptionNl = "Een kampeerplek in {end}, geschikt voor tenten of campers met toegang tot natuur en basisvoorzieningen."
    },
    new FakeProductType
    {
        Value = "event",
        NameEn = "Event in {end}",
        NameNl = "Evenement in {end}",
        DescriptionEn = "A ticket to a special event in {end}, such as a concert, festival, or cultural performance.",
        DescriptionNl = "Een ticket voor een speciaal evenement in {end}, zoals een concert, festival of culturele voorstelling."
    },
    new FakeProductType
    {
        Value = "bus",
        NameEn = "Bus Trip from {start} to {end}",
        NameNl = "Busreis van {start} naar {end}",
        DescriptionEn = "A bus ticket from {start} to {end}, providing comfortable seating and scheduled departures.",
        DescriptionNl = "Een busticket van {start} naar {end}, met comfortabele zitplaatsen en geplande vertrektijden."
    },
    new FakeProductType
    {
        Value = "apartment",
        NameEn = "Apartment Rental in {end}",
        NameNl = "Appartementverhuur in {end}",
        DescriptionEn = "A fully furnished apartment in {end}, available for short-term stays with kitchen and living areas.",
        DescriptionNl = "Een volledig gemeubileerd appartement in {end}, beschikbaar voor kort verblijf met keuken en woonruimtes."
    },
    new FakeProductType
    {
        Value = "activity",
        NameEn = "Activity in {end}",
        NameNl = "Activiteit in {end}",
        DescriptionEn = "A pre-booked activity in {end}, such as hiking, kayaking, or a cooking class.",
        DescriptionNl = "Een vooraf geboekte activiteit in {end}, zoals wandelen, kajakken of een kookworkshop."
    },
    new FakeProductType
    {
        Value = "ferry",
        NameEn = "Ferry from {start} to {end}",
        NameNl = "Veerboot van {start} naar {end}",
        DescriptionEn = "A ferry ticket from {start} to {end}, offering passenger or vehicle transport with onboard amenities.",
        DescriptionNl = "Een veerbootticket van {start} naar {end}, met passagiers- of voertuigtransport en voorzieningen aan boord."
    },
    new FakeProductType
    {
        Value = "resort",
        NameEn = "Resort Stay in {end}",
        NameNl = "Resortverblijf in {end}",
        DescriptionEn = "A resort package in {end}, including accommodations, dining, and access to pools or spas.",
        DescriptionNl = "Een resortpakket in {end}, inclusief accommodaties, dineren en toegang tot zwembaden of spa's."
    }
];

    public List<FakeProduct> GenerateProducts()
    {
        List<FakeProduct> products = [];
        Random random = new Random();
        
        // Ensure minimum products per location and type
        foreach (var type in Types)
        {
            foreach (var location in locations)
            {
                for (int i = 0; i < MinProductsPerLocationAndType; i++)
                {
                    if (products.Count >= ProductCount) break;
                    
                    var startLocation = locations[random.Next(locations.Count)];
                    var endLocation = location;
                    
                    // Ensure start and end locations are different
                    while (startLocation == endLocation)
                    {
                        endLocation = locations[random.Next(locations.Count)];
                    }
                    
                    var newProduct = new FakeProduct
                    {
                        StartLocation = startLocation,
                        EndLocation = endLocation,
                        ProductType = ProductType.Package,
                        Translations = new List<ProductTranslation>
                        {
                            new ProductTranslation 
                            { 
                                LangIsoCode = "en",
                                Name = type.NameEn.Replace("{start}", startLocation).Replace("{end}", endLocation),
                                Description = type.DescriptionEn.Replace("{start}", startLocation).Replace("{end}", endLocation),
                                Tags = new List<string>(),
                                IsActive = true,
                                ProductId = products.Count
                            },
                            new ProductTranslation 
                            { 
                                LangIsoCode = "nl", 
                                Name = type.NameNl.Replace("{start}", startLocation).Replace("{end}", endLocation),
                                Description = type.DescriptionNl.Replace("{start}", startLocation).Replace("{end}", endLocation),
                                Tags = new List<string>(),
                                IsActive = true,
                                ProductId = products.Count
                            }
                        },
                        ProductDates = GenerateProductDates(products.Count, random)
                    };
                    products.Add(newProduct);
                }
            }
        }
        
        // Fill remaining products up to ProductCount
        while (products.Count < ProductCount)
        {
            var type = Types[random.Next(Types.Count)];
            var startLocation = locations[random.Next(locations.Count)];
            var endLocation = locations[random.Next(locations.Count)];
            
            // Ensure start and end locations are different
            while (startLocation == endLocation)
            {
                endLocation = locations[random.Next(locations.Count)];
            }
            
            var newProduct = new FakeProduct
            {
                StartLocation = startLocation,
                EndLocation = endLocation,
                ProductType = ProductType.Package,
                Translations = new List<ProductTranslation>
                {
                    new ProductTranslation 
                    { 
                        LangIsoCode = "en", 
                        Name = type.NameEn.Replace("{start}", startLocation).Replace("{end}", endLocation),
                        Description = type.DescriptionEn.Replace("{start}", startLocation).Replace("{end}", endLocation),
                        Tags = new List<string>(),
                        IsActive = true,
                        ProductId = products.Count
                    },
                    new ProductTranslation
                    { 
                        LangIsoCode = "nl", 
                        Name = type.NameNl.Replace("{start}", startLocation).Replace("{end}", endLocation),
                        Description = type.DescriptionNl.Replace("{start}", startLocation).Replace("{end}", endLocation),
                        Tags = new List<string>(),
                        IsActive = true,
                        ProductId = products.Count
                    }
                },
                ProductDates = GenerateProductDates(products.Count, random)
            };
            products.Add(newProduct);
        }

        return products;
    }

    private static List<ProductDate> GenerateProductDates(int productId, Random random)
    {
        List<ProductDate> dates = [];
        var now = DateTime.Now;
        
        for (int i = 0; i < 50; i++)
        {
            var startDaysOffset = random.Next(0, 365); // Random start date within a year
            var duration = random.Next(1, 15); // Random duration 1-14 days
            var price = Math.Round(random.NextDouble() * (1000 - 50) + 50, 2); // Random price between 50 and 1000
            var slots = random.Next(50, 300); // Random slots between 50 and 300
            
            dates.Add(new ProductDate
            {
                Price = price,
                StartDate = now.AddDays(startDaysOffset),
                EndDate = now.AddDays(startDaysOffset + duration),
                Slots = slots,
                ProductId = productId
            });
        }
        
        return dates;
    }


    public void Up(DbContext dbContext)
    {
        var products = GenerateProducts();
        var productTranslationBatches = new List<List<ProductTranslation>>();
        var productDateBatches = new List<List<ProductDate>>();

        foreach (var product in products)
        {
            int typeId = ((int)product.ProductType);
            if (product.ProductType == null)
            {
                Console.WriteLine("Product type not found: " + product.ProductType);
                continue;
            }

            // Insert Product
            using (var productCmd = dbContext.CreateCommand())
            {
                // Escape string values to prevent basic SQL injection and syntax errors
                // This is a simplistic escaping and is NOT a substitute for proper parameterization.
                var startLocationEscaped = EscapeSqlString(product.StartLocation);
                var endLocationEscaped = EscapeSqlString(product.EndLocation);
                var deletedAtSql = "NULL";

                productCmd.CommandText = $@"
                    INSERT INTO `Products`(`StartLocation`, `EndLocation`, `DeletedAt`, `ProductTypeId`) 
                    VALUES ('{startLocationEscaped}', '{endLocationEscaped}', {deletedAtSql}, {typeId});
                    SELECT LAST_INSERT_ID();"; // For MySQL, use SCOPE_IDENTITY() for SQL Server

                var productId = Convert.ToInt32(productCmd.ExecuteScalar());

                // Prepare Product Translations for batch insert
                var currentProductTranslations = new List<ProductTranslation>();
                foreach (var productTranslation in product.Translations)
                {
                    productTranslation.ProductId = productId;
                    currentProductTranslations.Add(productTranslation);
                }
                productTranslationBatches.Add(currentProductTranslations);

                // Prepare Product Dates for batch insert
                var currentProductDates = new List<ProductDate>();
                foreach (var productDate in product.ProductDates)
                {
                    productDate.ProductId = productId;
                    currentProductDates.Add(productDate);
                }
                productDateBatches.Add(currentProductDates);

                Console.WriteLine("Prepared product for insertion: " + product.Translations[0].Name);
            }
        }

        // Perform batch insert for Product Translations
        foreach (var batch in productTranslationBatches)
        {
            if (batch.Any())
            {
                using (var translationCmd = dbContext.CreateCommand())
                {
                    var values = string.Join(",", batch.Select(pt =>
                    {
                        // Again, simplistic escaping.
                        var langIsoCodeEscaped = EscapeSqlString(pt.LangIsoCode);
                        var nameEscaped = EscapeSqlString(pt.Name);
                        var descriptionEscaped = EscapeSqlString(pt.Description ?? "NULL");
                        var tagsEscaped = JsonConvert.SerializeObject(pt.Tags);
                        var isActiveInt = pt.IsActive ? 1 : 0; // Convert boolean to integer for SQL

                        return $"('{langIsoCodeEscaped}', '{nameEscaped}', '{descriptionEscaped}', '{tagsEscaped}', {isActiveInt}, {pt.ProductId})";
                    }));

                    translationCmd.CommandText = $@"
                        INSERT INTO `ProductTranslations`(`LangIsoCode`, `Name`, `Description`, `Tags`, `IsActive`, `ProductId`) 
                        VALUES {values};";

                    translationCmd.ExecuteNonQuery();
                }
            }
        }

        // Perform batch insert for Product Dates
        foreach (var batch in productDateBatches)
        {
            if (batch.Any())
            {
                using (var dateCmd = dbContext.CreateCommand())
                {
                    var values = string.Join(",", batch.Select(pd =>
                    {
                        var priceSql = pd.Price.ToString(System.Globalization.CultureInfo.InvariantCulture); // Ensure decimal format is correct for SQL
                        
                        // Convert DateTime to Unix timestamp (seconds since epoch)
                        var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        var startDateUnix = (long)(pd.StartDate.ToUniversalTime() - unixEpoch).TotalSeconds;
                        var endDateUnix = (long)(pd.EndDate.ToUniversalTime() - unixEpoch).TotalSeconds;

                        var isActiveInt = pd.IsActive ? 1 : 0;

                        return $"({priceSql}, {startDateUnix}, {endDateUnix}, {pd.Slots}, {isActiveInt}, {pd.ProductId})";
                    }));

                    dateCmd.CommandText = $@"
                        INSERT INTO `ProductDates`(`Price`, `StartDate`, `EndDate`, `Slots`, `IsActive`, `ProductId`) 
                        VALUES {values};";

                    dateCmd.ExecuteNonQuery();
                }
            }
        }

        Console.WriteLine("All products, translations, and product dates inserted successfully.");
    }
    
    private string EscapeSqlString(string? value)
    {
        if (value == null) return "NULL";
        // Replace single quotes with two single quotes
        return value.Replace("'", "''");
    }

}