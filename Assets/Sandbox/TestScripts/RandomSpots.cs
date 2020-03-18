using UnityEngine;
using UnityEngine.AI;

public class RandomSpots : MonoBehaviour
{
    public Transform[] spots;

    public GameObject cubinho;

    private NavMeshAgent test;

    void Start()
    {
        int x = (int)Random.Range(0, spots.Length);

        cubinho.transform.position = spots[x].position;

    }

    private void Update()
    {
        Debug.Log(Mathf.PingPong(Time.time * 2, 1));
    }
}
