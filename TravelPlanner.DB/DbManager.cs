using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB;

public class DbManager : DbContext
{

    public ITable<User> Users => this.GetTable<User>();
    public ITable<Product> Products => this.GetTable<Product>();
    public ITable<Customer> Customers => this.GetTable<Customer>();
    public ITable<Quotation> Quotations => this.GetTable<Quotation>();

}