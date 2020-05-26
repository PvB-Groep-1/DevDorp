using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InstantiateVillager))]
public class ResourceDistributor : MonoBehaviour
{
    [SerializeField] private float _villagerSpawnTime = 1;

    [SerializeField] private float _HappinessIncreasePerHappyVillager = 1;
    [SerializeField] private float _HappinessDecreasePerMadVillager = 2;

    InstantiateVillager _IV = null;

    private void Awake()
    {
        _IV = GetComponent<InstantiateVillager>();

        BuildingManager.InitializeBuildingAmount();
        ResourceManager.InitializeResourceAmounts();

        BuildingManager.OnHouseBuild += IncreaseVillagers;
        BlockProgrammingWindow.OnBuildBuilding += BuildingManager.IncreaseAmountBuilding;
    }

    private void Start()
    {
        IncreaseVillagers();
    }

    private void Update()
    {
        IncreaseHappiness();
    }

    private void IncreaseHappiness()
    {
        int amountBread = (int)ResourceManager.GetResourceAmount(ResourceType.bread);
        int amountVillagers = (int)ResourceManager.GetResourceAmount(ResourceType.villagers);

        float happinessIncrease = 0;

        if (amountVillagers <= amountBread)
        {
            happinessIncrease = (_HappinessIncreasePerHappyVillager * ResourceManager.GetResourceAmount(ResourceType.villagers)) * (Time.deltaTime / 60);

            ResourceManager.IncreaseResource(ResourceType.happiness, happinessIncrease);
        }
        else
        {
            int extraVillagers = (int)ResourceManager.GetResourceAmount(ResourceType.villagers) - (int)ResourceManager.GetResourceAmount(ResourceType.bread);
            float happinessDecrease = (extraVillagers * _HappinessDecreasePerMadVillager) * (Time.deltaTime / 60);

            happinessIncrease = (_HappinessIncreasePerHappyVillager * ResourceManager.GetResourceAmount(ResourceType.villagers)) * (Time.deltaTime / 60);

            if(happinessIncrease > happinessDecrease)
                ResourceManager.IncreaseResource(ResourceType.happiness, happinessIncrease);
            else if(happinessDecrease > happinessIncrease)
                ResourceManager.DecreaseHappiness(happinessDecrease);
        }
    }

    private void IncreaseVillagers()
    {
        if (ResourceManager.GetResourceAmount(ResourceType.villagers) < ResourceManager._totalAmountAllowedVillagers)
            StartCoroutine(SpawnNewVillager());
    }

    private IEnumerator SpawnNewVillager()
    {
        yield return new WaitForSeconds(_villagerSpawnTime);

        _IV.InstantiateRandomVillager();
        ResourceManager.IncreaseResource(ResourceType.villagers, 1);
        IncreaseVillagers();
    }
}
