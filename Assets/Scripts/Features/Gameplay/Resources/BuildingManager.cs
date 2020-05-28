using UnityEngine;
using CM.Events;

/// <summary>
/// This function keeps track of how many special buildings are build.
/// </summary>
public static class BuildingManager
{
    // The amount of houses currently build.
    private static int TotalAmountHouses = 0;
    // The amount of bakeries currently build.
    private static int TotalAmountBakery = 0;

    /// <summary>
    /// An event used to tell the game whenever there is build a new house.
    /// </summary>
    public static SimpleEvent OnHouseBuild;

    /// <summary>
    /// This function gets called at the start of the game and initializes the variables needed inside this class.
    /// </summary>
    public static void InitializeBuildingAmount()
    {
        TotalAmountHouses = GameObject.FindGameObjectsWithTag("House").Length;
        TotalAmountBakery = GameObject.FindGameObjectsWithTag("Baker").Length;
    }

    /// <summary>
    /// This function is used to request the amount of the given building are currently build in the village.
    /// </summary>
    /// <param name="building">Takes in the type of the building you want to get the amount of.</param>
    /// <returns>Returns how many buildings are build of the given building type.</returns>
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

    /// <summary>
    /// This function increases the variable that keeps track of how many buildings are build.
    /// </summary>
    /// <param name="building">Takes in the building type of which building the amount has been increased.</param>
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

    /// <summary>
    /// This function decreases the variable that keeps track of how many buildings are build.
    /// </summary>
    /// <param name="building">Takes in the building type of which building the amount has been decreased.</param>
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

/// <summary>
/// An enumerator for all the different resource types.
/// </summary>
public enum BuildingType { bakery, house, none }
