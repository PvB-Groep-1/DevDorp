using System.Collections;
using UnityEngine;

/// <summary>
/// This class controlls when a resource has to be increased or decreased.
/// </summary>
[RequireComponent(typeof(InstantiateVillager))]
public class ResourceDistributor : MonoBehaviour
{
    // The time between each villager to be spawned.
    [SerializeField] private float _villagerSpawnTime = 10;

    // The amount the happiness has to be increased with for each happy villager.
    [SerializeField] private float _HappinessIncreasePerHappyVillager = 1;
    // The amount the happiness has to be decreased with for each annoyed villager.
    [SerializeField] private float _HappinessDecreasePerMadVillager = 2;

    private void Awake()
    {
        BuildingManager.InitializeBuildingAmount();
        ResourceManager.InitializeResourceAmounts();

        BuildingManager.OnHouseBuild += IncreaseVillagers;
        BlockProgrammingWindow.OnBuildBuilding += BuildingManager.IncreaseAmountBuilding;
        BlockProgrammingWindow.OnDestroyBuilding += BuildingManager.DecreaseAmountBuilding;
    }

    private void Start()
    {
        IncreaseVillagers();
    }

    private void Update()
    {
        PassiveHappinessGainLoss();
    }

    /// <summary>
    /// This function passively increases or decreases the happiness according to each happy/annoyed villager.
    /// </summary>
    private void PassiveHappinessGainLoss()
    {
        int amountBread = (int)ResourceManager.GetResourceAmount(ResourceType.bread);
        int amountVillagers = (int)ResourceManager.GetResourceAmount(ResourceType.villagers);

        float happinessIncrease = 0;

        if (amountVillagers <= amountBread)
        {
            happinessIncrease = (_HappinessIncreasePerHappyVillager * ResourceManager.GetResourceAmount(ResourceType.villagers)) * (Time.deltaTime / 120);

            ResourceManager.IncreaseResource(ResourceType.happiness, happinessIncrease);
        }
        else
        {
            int extraVillagers = (int)ResourceManager.GetResourceAmount(ResourceType.villagers) - (int)ResourceManager.GetResourceAmount(ResourceType.bread);
            float happinessDecrease = (extraVillagers * _HappinessDecreasePerMadVillager) * (Time.deltaTime / 60);

            happinessIncrease = (_HappinessIncreasePerHappyVillager * ResourceManager.GetResourceAmount(ResourceType.villagers)) * (Time.deltaTime / 120);

            if (happinessIncrease > happinessDecrease)
            {
                happinessIncrease -= happinessDecrease;
                ResourceManager.IncreaseResource(ResourceType.happiness, happinessIncrease);
            }
            else if (happinessDecrease > happinessIncrease)
            {
                happinessDecrease -= happinessIncrease;
                ResourceManager.DecreaseHappiness(happinessDecrease);
            }
        }
    }

    /// <summary>
    /// This function checks if the game needs to spawn a new villager.
    /// </summary>
    private void IncreaseVillagers()
    {
        if (ResourceManager.GetResourceAmount(ResourceType.villagers) < ResourceManager._totalAmountAllowedVillagers)
            StartCoroutine(SpawnNewVillager());
    }

    /// <summary>
    /// This IEnumerator spawns a new villager on a set interfall.
    /// </summary>
    private IEnumerator SpawnNewVillager()
    {
        yield return new WaitForSeconds(_villagerSpawnTime);

        InstantiateVillager.InstantiateRandomVillager();
        ResourceManager.IncreaseResource(ResourceType.villagers, 1);
        IncreaseVillagers();
    }
}
