﻿using UnityEngine.AI;
using UnityEngine;

/// <summary>
/// This class instantiates a new villager.
/// </summary>
public class InstantiateVillager : MonoBehaviour
{
    // Array reference to all the possible villagers.
    [SerializeField]
    private GameObject[] _Villagers = null;

    private static GameObject[] _VillagersStatic = null;

    private void Awake()
    {
        _VillagersStatic = _Villagers;
    }

    /// <summary>
    /// This function spawns one of the possible villagers.
    /// </summary>
    public static void InstantiateRandomVillager()
    {
        

        int randomVillager = Random.Range(0, _VillagersStatic.Length);
        Vector3 randomPosition = new Vector3(
            Random.Range(0, 5),
            .6f,
            Random.Range(0, 5));

        Collider[] coll = Physics.OverlapBox(randomPosition, Vector3.one / 2);

        if (coll.Length == 0)
        {
            var villager = Instantiate(_VillagersStatic[randomVillager], randomPosition, Quaternion.identity);
            villager.GetComponent<NavMeshAgent>().Warp(randomPosition);
        }
    }
}
