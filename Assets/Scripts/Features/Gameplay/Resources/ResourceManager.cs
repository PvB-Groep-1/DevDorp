using UnityEngine;
using CM.Events;

public static class ResourceManager
{
    private static float _totalAmountHappiness = 30;

    private static int _totalAmountBread = 0;
    private static int _totalAmountVillagers = 0;
    private static int _amountVillagersPerHouse = 5;
    private static int _amountBreadPerBakery = 10;

    public static int _totalAmountAllowedVillagers = 0;

    public static FloatEvent OnHappinessChanged;

    public static void InitializeResourceAmounts()
    {
        CalculateNewResourceAmount(ResourceType.bread);
        CalculateNewResourceAmount(ResourceType.villagers);

        _totalAmountVillagers = GameObject.FindGameObjectsWithTag("Villager").Length;
    }

    public static float GetResourceAmount(ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.happiness:
                return _totalAmountHappiness;
            case ResourceType.bread:
                return _totalAmountBread;
            case ResourceType.villagers:
                return _totalAmountVillagers;
        }

        return 0;
    }

    public static void IncreaseResource(ResourceType resource, float amount)
    {
        switch (resource)
        {
            case ResourceType.happiness:
                if ((_totalAmountHappiness + amount) < 100)
                    _totalAmountHappiness += amount;
                else
                    _totalAmountHappiness = 100;

                OnHappinessChanged?.Invoke(_totalAmountHappiness);
                break;

            case ResourceType.villagers:
                _totalAmountVillagers += (int)amount;
                break;
        }
    }

    public static void DecreaseHappiness(float happiness)
    {
        if ((_totalAmountHappiness - happiness) > 0)
            _totalAmountHappiness -= happiness;
        else
            _totalAmountHappiness = 0;

        OnHappinessChanged?.Invoke(_totalAmountHappiness);
    }

    public static void CalculateNewResourceAmount(ResourceType resource)
    {
        switch (resource)
        {
            case ResourceType.bread:
                _totalAmountBread = _amountBreadPerBakery * BuildingManager.GetBuildingAmount(BuildingType.bakery);
                break;
            case ResourceType.villagers:
                _totalAmountAllowedVillagers = _amountVillagersPerHouse * BuildingManager.GetBuildingAmount(BuildingType.house);
                break;
        }
        
    }
}

public enum ResourceType { happiness, bread, villagers }
