using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controlls when a resource has to be increased or decreased.
/// </summary>
[RequireComponent(typeof(InstantiateVillager))]
public class ResourceDistributor : MonoBehaviour
{
    // The time between each villager to be spawned.
    [SerializeField]
    private float _villagerSpawnTime = 10;

    // The amount the happiness has to be increased with for each happy villager.
    [SerializeField]
    private float _HappinessIncreasePerHappyVillager = 1;

    // The amount the happiness has to be decreased with for each annoyed villager.
    [SerializeField]
    private float _HappinessDecreasePerMadVillager = 2;

    // The notification object.
    [SerializeField]
    private GameObject _ResourceNotification = null;

    // Array used to save all the villagers who are currently walking around.
    private GameObject[] villagers = null;

    // Array used to save all the notifications currently in the game scene.
    private List<GameObject> notifications = new List<GameObject>();

    // A check to see if any notifications have been spawned.
    private bool _instantiatedNotifications = false;

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

            if(_instantiatedNotifications)
            {
                for (int i = 0; i < notifications.Count; i++)
                {
                    Destroy(notifications[i]);
                }

                villagers = null;
                notifications.Clear();

                _instantiatedNotifications = false;
            }
        }
        else
        {
            int extraVillagers = (int)ResourceManager.GetResourceAmount(ResourceType.villagers) - (int)ResourceManager.GetResourceAmount(ResourceType.bread);
            float happinessDecrease = (extraVillagers * _HappinessDecreasePerMadVillager) * (Time.deltaTime / 60);

            if (!_instantiatedNotifications)
                SpawnNotifications(extraVillagers);

            NotificationFollowVillager();

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
    /// This function spawns the notifications.
    /// </summary>
    /// <param name="extraVillagers">Takes in the amount of notifications take have to be spawned.</param>
    private void SpawnNotifications(int extraVillagers)
    {
        _instantiatedNotifications = true;

        villagers = GameObject.FindGameObjectsWithTag("Villager");

        for (int i = 0; i < extraVillagers; i++)
        {
            Vector3 Screenpos = Camera.main.WorldToScreenPoint(villagers[i].transform.position);
            GameObject notification = Instantiate(_ResourceNotification, Screenpos, Quaternion.identity);

            notification.transform.parent = GameObject.FindGameObjectWithTag("NotificationContainer").transform;

            notifications.Add(notification);
        }
    }
    
    // This function makes the notifications follow the villager.
    private void NotificationFollowVillager()
    {
        for (int i = 0; i < notifications.Count; i++)
        {
            notifications[i].transform.position = Camera.main.WorldToScreenPoint(villagers[i].transform.position) + new Vector3(0, 135, 0);
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
