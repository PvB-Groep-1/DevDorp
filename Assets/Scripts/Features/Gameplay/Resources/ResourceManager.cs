using CM.Events;

public static class ResourceManager
{
    private static float _totalAmountHappiness = 30;
    private static float _totalAmountBread = 0;

    private static int _totalAmountVillagers = 0;
    private static int _amountVillagersPerHouse = 5;
    private static int _amountBreadPerBakery = 10;

    public static int _totalAmountAllowedVillagers = 0;

    public static FloatEvent OnHappinessChanged;

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

    public static void IncreaseHappiness(float happiness)
    {
        if ((_totalAmountHappiness + happiness) < 100)
            _totalAmountHappiness += happiness;
        else
            _totalAmountHappiness = 100;

        OnHappinessChanged?.Invoke(_totalAmountHappiness);
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
