using UnityEngine;
using CM.Events;
public static class BuildingManager
{
    private static int TotalAmountHouses = 0;
    private static int TotalAmountBakery = 0;

    public static SimpleEvent OnHouseBuild;

    public static void InitializeBuildingAmount()
    {
        TotalAmountHouses = GameObject.FindGameObjectsWithTag("House").Length;
        TotalAmountBakery = GameObject.FindGameObjectsWithTag("Baker").Length;
    }

    public static int GetBuildingAmount(BuildingType building)
    {
        switch (building)
        {
            case BuildingType.bakery:
                return TotalAmountBakery;

            case BuildingType.house:
                return TotalAmountHouses;
        }

        return 0;
    }

    public static void IncreaseAmountBuilding(BuildingTypes building)
    {
        BuildingType actualBuilding = BuildingType.none;

        if (building == BuildingTypes.Bakery)
            actualBuilding = BuildingType.bakery;
        else if (building.ToString().Contains("House"))
            actualBuilding = BuildingType.house;
        else
            actualBuilding = BuildingType.none;

        switch (actualBuilding)
        {
            case BuildingType.bakery:
                TotalAmountBakery++;
                ResourceManager.CalculateNewResourceAmount(ResourceType.bread);
                break;

            case BuildingType.house:
                TotalAmountHouses++;
                ResourceManager.CalculateNewResourceAmount(ResourceType.villagers);
                OnHouseBuild?.Invoke();
                break;

            case BuildingType.none:
                break;
        }
    }

    public static void DecreaseAmountBuilding(BuildingTypes building)
    {
        BuildingType actualBuilding = BuildingType.none;

        if (building == BuildingTypes.Bakery)
            actualBuilding = BuildingType.bakery;
        else if (building.ToString().Contains("House"))
            actualBuilding = BuildingType.house;
        else
            actualBuilding = BuildingType.none;

        switch (actualBuilding)
        {
            case BuildingType.bakery:
                TotalAmountBakery--;
                ResourceManager.CalculateNewResourceAmount(ResourceType.bread);
                break;

            case BuildingType.house:
                TotalAmountHouses--;
                ResourceManager.CalculateNewResourceAmount(ResourceType.villagers);
                OnHouseBuild?.Invoke();
                break;

            case BuildingType.none:
                break;
        }
    }
}

public enum BuildingType { bakery, house, none }
