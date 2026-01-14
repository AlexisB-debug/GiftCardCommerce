namespace PeriwinkleWhiskers.Controllers;

public class FleaMarketStandController
{
    public IActionResult Index()
    {
        List<IDatabaseConnection> databaseConnections = new List<IDatabaseConnection>
        {
            new IDatabaseConnection { Text = "Great Lakes Coffee" },
            new IDatabaseConnection { Text = "Petoskey Stone Petroleum" },
            new IDatabaseConnection { Text = "Robin Eggs Grocery"},
            new IDatabaseConnection { Text = "Sleeping Bear Cinema" },
            new IDatabaseConnection { Text = "Sweet Crabapple Tavern" },
            new IDatabaseConnection { Text = "White-Tailed Deer Department Store" },
        };
        
        ViewBag.DatabaseConnectionList = databaseConnections;

        return View();
    }
}