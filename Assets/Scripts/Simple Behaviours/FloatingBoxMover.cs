using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBoxMover : MonoBehaviour
{
    [SerializeField]
    List<Vector3> waypoints = new List<Vector3>();
    Vector3 currentWaypoint;
    int currentIndex;
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
        foreach(Transform child in transform.parent.GetComponentsInChildren<Transform>())
        {
                waypoints.Add(child.position);

        }

        currentIndex = 0;
        currentWaypoint = waypoints[currentIndex];
        SetNewWaypoint(currentWaypoint);


    }

    // Update is called once per frame
    void Update()
    {
        if(MyFunctions.Distance(transform.position, currentWaypoint) > 0.15f)
        {
            transform.parent.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
        else
        {
            NewPointReached();
        }
    }

    void NewPointReached()
    {
        if(currentIndex < waypoints.Count - 1)
        {
            currentIndex++;

        }
        else
        {
            currentIndex = 0;
        }
        currentWaypoint = waypoints[currentIndex];
        SetNewWaypoint(currentWaypoint);
    }

    void SetNewWaypoint(Vector3 position)
    {
        transform.LookAt(position);
       
    }
}
