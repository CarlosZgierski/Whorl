using UnityEngine;
using UnityEngine.AI;

public class TEST_NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface uhul;

    public GameObject []  paths;

    void Start()
    {
        for (int i = 0; i < paths.Length; i++)
        {
            int a = (int)Random.Range(0, 2);
            if (a == 1)
                paths[i].SetActive(false);
            else
                paths[i].SetActive(true);
        }

        //isso tem que vir depóis de todo o level ser feito, resumindo, tem q vir dps de tdo pronto
        uhul.BuildNavMesh();

    }

#if (UNITY_EDITOR)
    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            RebakeNavMesh();
        }
    }
#endif 

    public void RebakeNavMesh()
    {
        uhul.UpdateNavMesh(uhul.navMeshData);
    }

}
