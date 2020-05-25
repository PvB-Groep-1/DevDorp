using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InstantiateVillager))]
public class ResourceDistributor : MonoBehaviour
{
    [SerializeField] private float _villagerSpawnTime = 10;

    InstantiateVillager _IV = null;

    private void Awake()
    {
        _IV = GetComponent<InstantiateVillager>();

        BuildingManager.OnHouseBuild += IncreaseVillagers;
        //BlockProgrammingWindow.OnBuildBuilding += BuildingManager.IncreaseAmountBuilding;
        BlockProgrammingWindow.OnBuildBuilding += (test) => { Debug.Log(test); };
    }

    private void Update()
    {
        
    }

    private void IncreaseVillagers()
    {
        if(ResourceManager.GetResourceAmount(ResourceType.villagers) < ResourceManager._totalAmountAllowedVillagers)
        {
            StartCoroutine(SpawnNewVillager());
            Debug.Log("Heya");
        }
    }

    private IEnumerator SpawnNewVillager()
    {
        yield return new WaitForSeconds(_villagerSpawnTime);

        _IV.InstantiateRandomVillager();

        IncreaseVillagers();
    }
}
