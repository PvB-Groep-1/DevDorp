using UnityEngine.AI;
using UnityEngine;

public class InstantiateVillager : MonoBehaviour
{
    [SerializeField] private GameObject[] _Villagers = null;

    public void InstantiateRandomVillager()
    {
        int randomVillager = Random.Range(0, _Villagers.Length);
        Vector3 randomPosition = new Vector3(
            Random.Range(0, 5),
            .6f,
            Random.Range(0, 5));

        Collider[] coll = Physics.OverlapBox(randomPosition, Vector3.one / 2);

        if (coll.Length == 0)
        {
            var villager = Instantiate(_Villagers[randomVillager], randomPosition, Quaternion.identity);
            villager.GetComponent<NavMeshAgent>().Warp(randomPosition);
        }
    }
}
