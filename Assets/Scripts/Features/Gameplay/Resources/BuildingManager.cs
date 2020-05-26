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
        Debug.Log("Test");

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
                Debug.Log("Baker: " + TotalAmountBakery);
                break;

            case BuildingType.house:
                TotalAmountHouses++;
                ResourceManager.CalculateNewResourceAmount(ResourceType.villagers);
                OnHouseBuild?.Invoke();
                Debug.Log("House: " + TotalAmountHouses);
                break;

            case BuildingType.none:
                break;
        }
    }

    public static void DecreaseAmountBuilding(BuildingType building, int amount)
    {
        switch (building)
        {
            case BuildingType.bakery:

                if ((TotalAmountBakery - amount) >= 0)
                    TotalAmountBakery -= amount;
                else
                    TotalAmountBakery = 0;

                break;

            case BuildingType.house:

                if ((TotalAmountHouses - amount) >= 0)
                    TotalAmountHouses -= amount;
                else
                    TotalAmountHouses = 0;

                break;
        }
    }
}

public enum BuildingType { bakery, house, none }
