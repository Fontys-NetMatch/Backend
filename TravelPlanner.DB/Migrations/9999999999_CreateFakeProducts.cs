using System.Text;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;
using TravelPlanner.Domain.Models.GenerationModels;

namespace TravelPlanner.DB.Migrations;

[DevOnly]
public class CreateFakeProducts : IMigration
{

    private List<ProductType> ProductTypes = [
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Flight", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Vlucht", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Hotel", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Hotel", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Hostel", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Hostel", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Car", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Auto", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Exhibit", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Tentoonstelling", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Cruise", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Cruise", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Bus", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Bus", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Train", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Trein", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Restaurant Booking", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Restaurantreservering", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Event", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Evenement", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Insurance", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Verzekering", IsActive = true, ProductType_ID = 0 }
            }
        },
        new ProductType
        {
            IsActive = true,
            Translations = new List<ProductTypeTranslation>
            {
                new ProductTypeTranslation { LangIsoCode = "en", Name = "Guided Tour", IsActive = true, ProductType_ID = 0 },
                new ProductTypeTranslation { LangIsoCode = "nl", Name = "Gids Tour", IsActive = true, ProductType_ID = 0 }
            }
        }
    ];

    private List<FakeProduct> Products = [
        new FakeProduct
        {
            StartLocation = "Amsterdam",
            EndLocation = "New York",
            ProductType_Name = "Flight",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Flight to NYC", Description = "Direct flight from Amsterdam to New York.", Tags = new List<string>(){ "flight", "usa", "travel" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Vlucht naar NYC", Description = "Directe vlucht van Amsterdam naar New York.", Tags = new List<string>(){ "vlucht", "vs", "reizen" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 19.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 250, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 200, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 150, Product_ID = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 100, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Berlin",
            EndLocation = "Munich",
            ProductType_Name = "Train",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Train Berlin to Munich", Description = "High-speed train journey through Germany.", Tags = new List<string>(){ "train", "germany", "fast" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Trein van Berlijn naar München", Description = "Hogesnelheidstrein door Duitsland.", Tags = new List<string>(){ "trein", "duitsland", "snel" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Rome",
            EndLocation = "Rome",
            ProductType_Name = "Hotel",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Rome Grand Hotel", Description = "5-star luxury hotel in the heart of Rome.", Tags = new List<string>(){ "hotel", "rome", "luxury" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Rome Grand Hotel", Description = "5-sterren luxe hotel in het hart van Rome.", Tags = new List<string>(){ "hotel", "rome", "luxe" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 199.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 50, Product_ID = 0 },
                new ProductDate { Price = 249.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 40, Product_ID = 0 },
                new ProductDate { Price = 299.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 30, Product_ID = 0 },
                new ProductDate { Price = 349.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 20, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Paris",
            EndLocation = "Barcelona",
            ProductType_Name = "Bus",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Bus Paris to Barcelona", Description = "Affordable ride from Paris to Barcelona.", Tags = new List<string>(){ "bus", "cheap", "spain" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Bus Parijs naar Barcelona", Description = "Betaalbare rit van Parijs naar Barcelona.", Tags = new List<string>(){ "bus", "goedkoop", "spanje" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 29.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 150, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 120, Product_ID = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 59.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 80, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "London",
            EndLocation = "London",
            ProductType_Name = "Car",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "London Car Rental", Description = "Rent a car in London for your adventure.", Tags = new List<string>(){ "car", "rental", "london" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Autoverhuur Londen", Description = "Huur een auto in Londen voor je avontuur.", Tags = new List<string>(){ "auto", "huur", "londen" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 49.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 20, Product_ID = 0 },
                new ProductDate { Price = 59.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 15, Product_ID = 0 },
                new ProductDate { Price = 69.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 10, Product_ID = 0 },
                new ProductDate { Price = 79.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 5, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Oslo",
            EndLocation = "Copenhagen",
            ProductType_Name = "Cruise",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Oslo-Copenhagen Cruise", Description = "Beautiful fjord cruise between Oslo and Copenhagen.", Tags = new List<string>(){ "cruise", "norway", "denmark" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Cruise Oslo-Kopenhagen", Description = "Prachtige fjordencruise tussen Oslo en Kopenhagen.", Tags = new List<string>(){ "cruise", "noorwegen", "denemarken" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 99.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 50, Product_ID = 0 },
                new ProductDate { Price = 109.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 40, Product_ID = 0 },
                new ProductDate { Price = 119.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 30, Product_ID = 0 },
                new ProductDate { Price = 129.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 20, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Madrid",
            EndLocation = "Lisbon",
            ProductType_Name = "Guided Tour",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Madrid Walking Tour", Description = "Guided tour through the historic streets of Madrid.", Tags = new List<string>(){ "tour", "spain", "guide" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Wandeltocht Madrid", Description = "Gids tour door de historische straten van Madrid.", Tags = new List<string>(){ "tour", "spanje", "gids" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 29.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 59.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Prague",
            EndLocation = "Vienna",
            ProductType_Name = "Exhibit",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Budget Hostel in Prague", Description = "Stay in a cozy hostel in Prague.", Tags = new List<string>(){ "hostel", "prague", "budget" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Budget Hostel in Praag", Description = "Verblijf in een gezellig hostel in Praag.", Tags = new List<string>(){ "hostel", "praag", "budget" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 19.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 50, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 40, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 30, Product_ID = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 20, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Athens",
            EndLocation = "Santorini",
            ProductType_Name = "Flight",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Flight Athens to Santorini", Description = "Fly between Greek islands.", Tags = new List<string>(){ "flight", "greece", "islands" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Vlucht Athene naar Santorini", Description = "Vlieg tussen de Griekse eilanden.", Tags = new List<string>(){ "vlucht", "griekenland", "eilanden" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 49.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 59.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 69.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 79.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Brussels",
            EndLocation = "Antwerp",
            ProductType_Name = "Event",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Live Music in Brussels", Description = "Event with top artists performing live.", Tags = new List<string>(){ "event", "music", "brussels" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Live Muziek in Brussel", Description = "Evenement met topartiesten live op het podium.", Tags = new List<string>(){ "evenement", "muziek", "brussel" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 19.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 200, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 150, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 50, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Zurich",
            EndLocation = "Geneva",
            ProductType_Name = "Restaurant Booking",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Restaurant Reservation Zurich", Description = "Book a table in top Zurich restaurants.", Tags = new List<string>(){ "food", "reservation", "zurich" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Restaurantreservering Zürich", Description = "Reserveer een tafel in toprestaurants in Zürich.", Tags = new List<string>(){ "eten", "reservering", "zürich" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 29.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 59.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Helsinki",
            EndLocation = "Stockholm",
            ProductType_Name = "Exhibit",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Art Exhibit in Stockholm", Description = "Explore modern art in the Nordic region.", Tags = new List<string>(){ "exhibit", "art", "sweden" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Kunsttentoonstelling in Stockholm", Description = "Ontdek moderne kunst in Scandinavië.", Tags = new List<string>(){ "tentoonstelling", "kunst", "zweden" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 49.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Dublin",
            EndLocation = "Cork",
            ProductType_Name = "Insurance",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Travel Insurance Ireland", Description = "Comprehensive insurance for your trip.", Tags = new List<string>(){ "insurance", "ireland", "travel" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Reisverzekering Ierland", Description = "Uitgebreide verzekering voor je reis.", Tags = new List<string>(){ "verzekering", "ierland", "reizen" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Venice",
            EndLocation = "Florence",
            ProductType_Name = "Hotel",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Venice Lagoon Hotel", Description = "Charming stay on the canals of Venice.", Tags = new List<string>(){ "hotel", "venice", "romantic" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Hotel Venetië Lagoon", Description = "Charmant verblijf aan de kanalen van Venetië.", Tags = new List<string>(){ "hotel", "venetië", "romantisch" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Nice",
            EndLocation = "Marseille",
            ProductType_Name = "Hostel",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Hostel near Marseille Beach", Description = "Budget-friendly hostel by the sea.", Tags = new List<string>(){ "hostel", "marseille", "beach" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Hostel bij het strand van Marseille", Description = "Budgetvriendelijk hostel aan zee.", Tags = new List<string>(){ "hostel", "marseille", "strand" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Frankfurt",
            EndLocation = "Berlin",
            ProductType_Name = "Flight",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Frankfurt-Berlin Flight", Description = "Business-class flight within Germany.", Tags = new List<string>(){ "flight", "berlin", "germany" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Vlucht Frankfurt-Berlijn", Description = "Businessclass vlucht binnen Duitsland.", Tags = new List<string>(){ "vlucht", "berlijn", "duitsland" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Edinburgh",
            EndLocation = "Glasgow",
            ProductType_Name = "Train",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Scenic Train in Scotland", Description = "Ride through the Scottish highlands.", Tags = new List<string>(){ "train", "scotland", "view" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Pittoreske treinreis in Schotland", Description = "Reis door de Schotse Hooglanden.", Tags = new List<string>(){ "trein", "schotland", "uitzicht" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Reykjavik",
            EndLocation = "Akureyri",
            ProductType_Name = "Guided Tour",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Icelandic Adventure Tour", Description = "Explore volcanoes and glaciers with a guide.", Tags = new List<string>(){ "tour", "iceland", "adventure" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "IJslandse Avontuurlijke Tour", Description = "Ontdek vulkanen en gletsjers met een gids.", Tags = new List<string>(){ "tour", "ijsland", "avontuur" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Warsaw",
            EndLocation = "Krakow",
            ProductType_Name = "Bus",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Polish Express Bus", Description = "Efficient travel between Warsaw and Krakow.", Tags = new List<string>(){ "bus", "poland", "express" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Poolse Expressbus", Description = "Efficiënt reizen tussen Warschau en Krakau.", Tags = new List<string>(){ "bus", "polen", "snel" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 29.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Lisbon",
            EndLocation = "Porto",
            ProductType_Name = "Car",
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Lisbon Car Hire", Description = "Rent a car and explore the coast.", Tags = new List<string>(){ "car", "lisbon", "hire" }, IsActive = true, Product_ID = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Autohuur Lissabon", Description = "Huur een auto en ontdek de kust.", Tags = new List<string>(){ "auto", "lissabon", "huur" }, IsActive = true, Product_ID = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, Product_ID = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, Product_ID = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, Product_ID = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, Product_ID = 0 }
            ]
        }
    ];

    public void Up(DbContext dbContext)
    {

        foreach (var productType in ProductTypes)
        {
            var productTypeId = dbContext.InsertWithInt32Identity(productType);
            productType.ID = productTypeId;

            foreach (var productTypeTranslation in productType.Translations)
            {
                productTypeTranslation.ProductType_ID = productTypeId;
                dbContext.InsertWithInt32Identity(productTypeTranslation);
            }

            Console.WriteLine("Inserted product type: " + productType.Translations[0].Name);
        }

        foreach (var product in Products)
        {
            var productType = ProductTypes.Find(x => x.Translations[0].Name == product.ProductType_Name);
            if (productType == null)
            {
                Console.WriteLine("Product type not found: " + product.ProductType_Name);
                continue;
            }

            var dbProduct = new Product
            {
                StartLocation = product.StartLocation,
                EndLocation = product.EndLocation,
                ProductType_ID = productType.ID,
                DeletedAt = null
            };
            var productId = dbContext.InsertWithInt32Identity(dbProduct);

            foreach (var productTranslation in product.Translations)
            {
                productTranslation.Product_ID = productId;
                dbContext.InsertWithInt32Identity(productTranslation);
            }

            foreach (var productDate in product.ProductDates)
            {
                productDate.Product_ID = productId;
                dbContext.InsertWithInt32Identity(productDate);
            }

            Console.WriteLine("Inserted product: " + product.Translations[0].Name);
        }

        Console.WriteLine("All products and product types inserted successfully.");
    }

}