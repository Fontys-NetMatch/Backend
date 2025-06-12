using System.Text;
using LinqToDB;
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

    private List<FakeProduct> Products = [
        new FakeProduct
        {
            StartLocation = "Amsterdam",
            EndLocation = "New York",
            ProductType = ProductType.Package,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Flight to NYC", Description = "Direct flight from Amsterdam to New York.", Tags = new List<string>(){ "flight", "usa", "travel" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Vlucht naar NYC", Description = "Directe vlucht van Amsterdam naar New York.", Tags = new List<string>(){ "vlucht", "vs", "reizen" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 19.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 250, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 200, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 150, ProductId = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 100, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Berlin",
            EndLocation = "Munich",
            ProductType = ProductType.HolidayPark,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Train Berlin to Munich", Description = "High-speed train journey through Germany.", Tags = new List<string>(){ "train", "germany", "fast" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Trein van Berlijn naar München", Description = "Hogesnelheidstrein door Duitsland.", Tags = new List<string>(){ "trein", "duitsland", "snel" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Rome",
            EndLocation = "Rome",
            ProductType = ProductType.Hotel,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Rome Grand Hotel", Description = "5-star luxury hotel in the heart of Rome.", Tags = new List<string>(){ "hotel", "rome", "luxury" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Rome Grand Hotel", Description = "5-sterren luxe hotel in het hart van Rome.", Tags = new List<string>(){ "hotel", "rome", "luxe" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 199.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 50, ProductId = 0 },
                new ProductDate { Price = 249.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 40, ProductId = 0 },
                new ProductDate { Price = 299.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 30, ProductId = 0 },
                new ProductDate { Price = 349.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 20, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Paris",
            EndLocation = "Barcelona",
            ProductType = ProductType.Package,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Bus Paris to Barcelona", Description = "Affordable ride from Paris to Barcelona.", Tags = new List<string>(){ "bus", "cheap", "spain" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Bus Parijs naar Barcelona", Description = "Betaalbare rit van Parijs naar Barcelona.", Tags = new List<string>(){ "bus", "goedkoop", "spanje" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 29.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 150, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 120, ProductId = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 59.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 80, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "London",
            EndLocation = "London",
            ProductType = ProductType.Package,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "London Car Rental", Description = "Rent a car in London for your adventure.", Tags = new List<string>(){ "car", "rental", "london" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Autoverhuur Londen", Description = "Huur een auto in Londen voor je avontuur.", Tags = new List<string>(){ "auto", "huur", "londen" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 49.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 20, ProductId = 0 },
                new ProductDate { Price = 59.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 15, ProductId = 0 },
                new ProductDate { Price = 69.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 10, ProductId = 0 },
                new ProductDate { Price = 79.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 5, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Oslo",
            EndLocation = "Copenhagen",
            ProductType = ProductType.HolidayPark,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Oslo-Copenhagen Cruise", Description = "Beautiful fjord cruise between Oslo and Copenhagen.", Tags = new List<string>(){ "cruise", "norway", "denmark" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Cruise Oslo-Kopenhagen", Description = "Prachtige fjordencruise tussen Oslo en Kopenhagen.", Tags = new List<string>(){ "cruise", "noorwegen", "denemarken" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 99.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 50, ProductId = 0 },
                new ProductDate { Price = 109.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 40, ProductId = 0 },
                new ProductDate { Price = 119.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 30, ProductId = 0 },
                new ProductDate { Price = 129.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 20, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Madrid",
            EndLocation = "Lisbon",
            ProductType = ProductType.Hotel,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Madrid Walking Tour", Description = "Guided tour through the historic streets of Madrid.", Tags = new List<string>(){ "tour", "spain", "guide" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Wandeltocht Madrid", Description = "Gids tour door de historische straten van Madrid.", Tags = new List<string>(){ "tour", "spanje", "gids" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 29.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 59.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Prague",
            EndLocation = "Vienna",
            ProductType = ProductType.HolidayPark,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Budget Hostel in Prague", Description = "Stay in a cozy hostel in Prague.", Tags = new List<string>(){ "hostel", "prague", "budget" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Budget Hostel in Praag", Description = "Verblijf in een gezellig hostel in Praag.", Tags = new List<string>(){ "hostel", "praag", "budget" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 19.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 50, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 40, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 30, ProductId = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 20, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Athens",
            EndLocation = "Santorini",
            ProductType = ProductType.Package,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Flight Athens to Santorini", Description = "Fly between Greek islands.", Tags = new List<string>(){ "flight", "greece", "islands" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Vlucht Athene naar Santorini", Description = "Vlieg tussen de Griekse eilanden.", Tags = new List<string>(){ "vlucht", "griekenland", "eilanden" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 49.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 59.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 69.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 79.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Brussels",
            EndLocation = "Antwerp",
            ProductType = ProductType.Hotel,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Live Music in Brussels", Description = "Event with top artists performing live.", Tags = new List<string>(){ "event", "music", "brussels" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Live Muziek in Brussel", Description = "Evenement met topartiesten live op het podium.", Tags = new List<string>(){ "evenement", "muziek", "brussel" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 19.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 200, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 150, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 50, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Zurich",
            EndLocation = "Geneva",
            ProductType = ProductType.Package,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Restaurant Reservation Zurich", Description = "Book a table in top Zurich restaurants.", Tags = new List<string>(){ "food", "reservation", "zurich" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Restaurantreservering Zürich", Description = "Reserveer een tafel in toprestaurants in Zürich.", Tags = new List<string>(){ "eten", "reservering", "zürich" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 29.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 59.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Helsinki",
            EndLocation = "Stockholm",
            ProductType = ProductType.HolidayPark,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Art Exhibit in Stockholm", Description = "Explore modern art in the Nordic region.", Tags = new List<string>(){ "exhibit", "art", "sweden" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Kunsttentoonstelling in Stockholm", Description = "Ontdek moderne kunst in Scandinavië.", Tags = new List<string>(){ "tentoonstelling", "kunst", "zweden" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 49.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 49.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Dublin",
            EndLocation = "Cork",
            ProductType = ProductType.Package,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Travel Insurance Ireland", Description = "Comprehensive insurance for your trip.", Tags = new List<string>(){ "insurance", "ireland", "travel" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Reisverzekering Ierland", Description = "Uitgebreide verzekering voor je reis.", Tags = new List<string>(){ "verzekering", "ierland", "reizen" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Venice",
            EndLocation = "Florence",
            ProductType = ProductType.Hotel,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Venice Lagoon Hotel", Description = "Charming stay on the canals of Venice.", Tags = new List<string>(){ "hotel", "venice", "romantic" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Hotel Venetië Lagoon", Description = "Charmant verblijf aan de kanalen van Venetië.", Tags = new List<string>(){ "hotel", "venetië", "romantisch" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Nice",
            EndLocation = "Marseille",
            ProductType = ProductType.Package,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Hostel near Marseille Beach", Description = "Budget-friendly hostel by the sea.", Tags = new List<string>(){ "hostel", "marseille", "beach" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Hostel bij het strand van Marseille", Description = "Budgetvriendelijk hostel aan zee.", Tags = new List<string>(){ "hostel", "marseille", "strand" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Frankfurt",
            EndLocation = "Berlin",
            ProductType = ProductType.Hotel,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Frankfurt-Berlin Flight", Description = "Business-class flight within Germany.", Tags = new List<string>(){ "flight", "berlin", "germany" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Vlucht Frankfurt-Berlijn", Description = "Businessclass vlucht binnen Duitsland.", Tags = new List<string>(){ "vlucht", "berlijn", "duitsland" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Edinburgh",
            EndLocation = "Glasgow",
            ProductType = ProductType.HolidayPark,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Scenic Train in Scotland", Description = "Ride through the Scottish highlands.", Tags = new List<string>(){ "train", "scotland", "view" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Pittoreske treinreis in Schotland", Description = "Reis door de Schotse Hooglanden.", Tags = new List<string>(){ "trein", "schotland", "uitzicht" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Reykjavik",
            EndLocation = "Akureyri",
            ProductType = ProductType.Package,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Icelandic Adventure Tour", Description = "Explore volcanoes and glaciers with a guide.", Tags = new List<string>(){ "tour", "iceland", "adventure" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "IJslandse Avontuurlijke Tour", Description = "Ontdek vulkanen en gletsjers met een gids.", Tags = new List<string>(){ "tour", "ijsland", "avontuur" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Warsaw",
            EndLocation = "Krakow",
            ProductType = ProductType.Package,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Polish Express Bus", Description = "Efficient travel between Warsaw and Krakow.", Tags = new List<string>(){ "bus", "poland", "express" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Poolse Expressbus", Description = "Efficiënt reizen tussen Warschau en Krakau.", Tags = new List<string>(){ "bus", "polen", "snel" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 29.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        },
        new FakeProduct
        {
            StartLocation = "Lisbon",
            EndLocation = "Porto",
            ProductType = ProductType.Hotel,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation { LangIsoCode = "en", Name = "Lisbon Car Hire", Description = "Rent a car and explore the coast.", Tags = new List<string>(){ "car", "lisbon", "hire" }, IsActive = true, ProductId = 0 },
                new ProductTranslation { LangIsoCode = "nl", Name = "Autohuur Lissabon", Description = "Huur een auto en ontdek de kust.", Tags = new List<string>(){ "auto", "lissabon", "huur" }, IsActive = true, ProductId = 0 }
            },
            ProductDates = [
                new ProductDate { Price = 9.99, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7), Slots = 100, ProductId = 0 },
                new ProductDate { Price = 19.99, StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(20), Slots = 80, ProductId = 0 },
                new ProductDate { Price = 29.99, StartDate = DateTime.Now.AddDays(30), EndDate = DateTime.Now.AddDays(40), Slots = 60, ProductId = 0 },
                new ProductDate { Price = 39.99, StartDate = DateTime.Now.AddDays(50), EndDate = DateTime.Now.AddDays(60), Slots = 40, ProductId = 0 }
            ]
        }
    ];

    public void Up(DbContext dbContext)
    {



        foreach (var product in Products)
        {
            int TypeID = ((int)product.ProductType);
            if (product.ProductType == null)
            {
                Console.WriteLine("Product type not found: " + product.ProductType);
                continue;
            }

            var dbProduct = new Product
            {
                StartLocation = product.StartLocation,
                EndLocation = product.EndLocation,
                ProductTypeId = TypeID,
                DeletedAt = null
            };
            var productId = dbContext.InsertWithInt32Identity(dbProduct);

            foreach (var productTranslation in product.Translations)
            {
                productTranslation.ProductId = productId;
                dbContext.InsertWithInt32Identity(productTranslation);
            }

            foreach (var productDate in product.ProductDates)
            {
                productDate.ProductId = productId;
                dbContext.InsertWithInt32Identity(productDate);
            }

            Console.WriteLine("Inserted product: " + product.Translations[0].Name);
        }

        Console.WriteLine("All products and product types inserted successfully.");
    }

}