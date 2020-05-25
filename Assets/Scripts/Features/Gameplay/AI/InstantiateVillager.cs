using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateVillager : MonoBehaviour
{
    [SerializeField] private GameObject[] _Villagers = null;

    public void InstantiateRandomVillager()
    {
        int randomVillager = Random.Range(0, _Villagers.Length);
        Vector3 randomPosition = new Vector3(
            Random.Range(-43, 74),
            Random.Range(-12, 92));

        Collider[] coll = Physics.OverlapBox(randomPosition, Vector3.one / 2);

        if (coll.Length == 0)
        {
            Instantiate(_Villagers[randomVillager], transform.position, Quaternion.identity);
        }
    }
}
