using System.Runtime.InteropServices.JavaScript;

namespace PeriwinkleWhiskers;

public static class DatabaseAccessFactory
{
    public static IDatabaseConnection GetTargetDatabaseAccess(string targetDatabase)
    {
        switch (targetDatabase)
        {
            case "Great Lakes Coffee":
                return string GreatLakesDatabaseConn = JObject.Parse(File.ReadAllText("appsettings.json")).GetValue("GreatLakesDatabaseConn")?.ToString() ?? "";
            case "Petoskey Stone Petroleum":
                return string PetoskeyStoneDatabaseConn = JObject.Parse(File.ReadAllText("appsettings.json")).GetValue("PetoskeyStoneDatabaseConn")?.ToString() ?? "";
            case "Robin Eggs Grocery":
                return string RobinEggsDatabaseConn = JObject.Parse(File.ReadAllText("appsettings.json")).GetValue("RobinEggsDatabaseConn")?.ToString() ?? "";
            case "Sleeping Bear Cinema":
                return string SleepingBearDatabaseConn = JObject.Parse(File.ReadAllText("appsettings.json")).GetValue("SleepingBearDatabaseConn")?.ToString() ?? "";
            case "Sweet Crabapple Tavern":
                return string SweetCrabappleDatabaseConn = JObject.Parse(File.ReadAllText("appsettings.json")).GetValue("SweetCrabappleDatabaseConn")?.ToString() ?? "";
            case "White-Tailed Deer Department Store":
                return string White-TailedDeerDatabaseConn() = JObject.Parse(File.ReadAllText("appsettings.json")).GetValue("White-TailedDeerDatabaseConn")?.ToString() ?? "";
            default:
        }
    }
}