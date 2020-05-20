public static class ResourceManager
{
    public static float TotalHappiness = 30;

    public static void IncreaseHappiness(float happiness)
    {
        if ((TotalHappiness + happiness) < 100)
            TotalHappiness += happiness;
        else
            TotalHappiness = 100;
    }

    public static void DecreaseHappiness(float happiness)
    {
        if ((TotalHappiness - happiness) > 0)
            TotalHappiness -= happiness;
        else
            TotalHappiness = 0;
    }
}
