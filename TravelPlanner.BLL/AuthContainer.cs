using TravelPlanner.DB;

namespace TravelPlanner.DAL;

public class AuthContainer
{

    private readonly DbContext _db;

    public AuthContainer(DbContext db)
    {
        _db = db;
    }

    public object RegisterRequest()
    {
        return new
        {

        };
    }

}