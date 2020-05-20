public static class BuildingManager
{
    private static int TotalAmountHouses = 0;
    private static int TotalAmountBakery = 0;

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

    public static void IncreaseAmountBuilding(BuildingType building, int amount)
    {
        switch (building)
        {
            case BuildingType.bakery:
                TotalAmountBakery += amount;
                break;

            case BuildingType.house:
                TotalAmountHouses += amount;
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

public enum BuildingType { bakery, house }
