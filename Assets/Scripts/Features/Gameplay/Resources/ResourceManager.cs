using CM.Events;

public static class ResourceManager
{
    private static float TotalAmountHappiness = 30;
    private static float TotalAmountBread = 0;

    public static FloatEvent OnHappinessChanged;

    public static float GetResourceAmount()
    {
        return TotalAmountHappiness;
    }

    public static void IncreaseHappiness(float happiness)
    {
        if ((TotalAmountHappiness + happiness) < 100)
            TotalAmountHappiness += happiness;
        else
            TotalAmountHappiness = 100;

        OnHappinessChanged?.Invoke(TotalAmountHappiness);
    }

    public static void DecreaseHappiness(float happiness)
    {
        if ((TotalAmountHappiness - happiness) > 0)
            TotalAmountHappiness -= happiness;
        else
            TotalAmountHappiness = 0;

        OnHappinessChanged?.Invoke(TotalAmountHappiness);
    }
}
