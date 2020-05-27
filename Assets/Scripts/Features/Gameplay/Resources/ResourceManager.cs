using UnityEngine;
using CM.Events;

/// <summary>
/// This function is the brain of the resource system.
/// </summary>
public static class ResourceManager
{
    // The amount of happiness that the city currently has.
    private static float _currentAmountHappiness = 30;

    // The amount of bread the player currently has.
    private static int _currentAmountBread = 0;
    // The amount of villagers currently living in the village.
    private static int _currentAmountVillagers = 0;
    // The total amount of villagers each house can hold.
    private static int _amountVillagersPerHouse = 5;
    // The total amount of bread each bakery can hold.
    private static int _amountBreadPerBakery = 10;

    /// <summary>
    /// The total amount of villagers that can settle in the village.
    /// </summary>
    public static int _totalAmountAllowedVillagers = 0;

    /// <summary>
    /// An event used to tell the game that the happiness amount changed.
    /// </summary>
    public static FloatEvent OnHappinessChanged;

    /// <summary>
    /// This function gets called at the start of the game and initializes the variables needed inside this class.
    /// </summary>
    public static void InitializeResourceAmounts()
    {
        CalculateNewResourceAmount(ResourceType.bread);
        CalculateNewResourceAmount(ResourceType.villagers);

        _currentAmountVillagers = GameObject.FindGameObjectsWithTag("Villager").Length;
    }

    /// <summary>
    /// This function can be called to get the amount of any resource.
    /// </summary>
    /// <param name="resourceType">Takes in the type of the resource you want to use.</param>
    /// <returns>Returns the amount of the requested resource.</returns>
    public static float GetResourceAmount(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.happiness:
                return _currentAmountHappiness;
            case ResourceType.bread:
                return _currentAmountBread;
            case ResourceType.villagers:
                return _currentAmountVillagers;
        }

        return 0;
    }

    /// <summary>
    /// This function increases the amount of the given resource type by the given amount.
    /// </summary>
    /// <param name="resource">Takes in the type of resource that needs to be increased.</param>
    /// <param name="amount">Takes in the amount with which the given resource has to be increased.</param>
    public static void IncreaseResource(ResourceType resource, float amount)
    {
        switch (resource)
        {
            case ResourceType.happiness:
                if ((_currentAmountHappiness + amount) < 100)
                    _currentAmountHappiness += amount;
                else
                    _currentAmountHappiness = 100;

                OnHappinessChanged?.Invoke(_currentAmountHappiness);
                break;

            case ResourceType.villagers:
                _currentAmountVillagers += (int)amount;
                break;
        }
    }

    /// <summary>
    /// This funtion decreases the amount of happiness.
    /// </summary>
    /// <param name="happiness"></param>
    public static void DecreaseHappiness(float happiness)
    {
        if ((_currentAmountHappiness - happiness) > 0)
            _currentAmountHappiness -= happiness;
        else
            _currentAmountHappiness = 0;

        OnHappinessChanged?.Invoke(_currentAmountHappiness);
    }

    /// <summary>
    /// This function calculates the new max capacity of the given resource type.
    /// </summary>
    /// <param name="resource"></param>
    public static void CalculateNewResourceAmount(ResourceType resource)
    {
        switch (resource)
        {
            case ResourceType.bread:
                _currentAmountBread = _amountBreadPerBakery * BuildingManager.GetBuildingAmount(BuildingType.bakery);
                break;
            case ResourceType.villagers:
                _totalAmountAllowedVillagers = _amountVillagersPerHouse * BuildingManager.GetBuildingAmount(BuildingType.house);
                break;
        }
        
    }
}

/// <summary>
/// An enumerator for all the different resource types.
/// </summary>
public enum ResourceType { happiness, bread, villagers }
