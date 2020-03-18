using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeField] private NavMeshSurface mainNavMesh;

    [SerializeField] private bool BuildOnStart = true;
    //[SerializeField] private VariationController varControl_Ref;

    void Start()
    {
        if (mainNavMesh == null)
        {
            mainNavMesh = GetComponent<NavMeshSurface>();
        }
        if(BuildOnStart)
        StartCoroutine(WaitStartsAndAwakes());
    }

    public void BuildNavMesh()
    {
        mainNavMesh.BuildNavMesh();
    }

    public void RebakeNavMesh()
    {
        mainNavMesh.UpdateNavMesh(mainNavMesh.navMeshData);
    }

    private IEnumerator WaitStartsAndAwakes()
    {
        yield return new WaitForEndOfFrame();
        BuildNavMesh();
    }
}
