using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TestAgent : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent hoi;

    private bool following;

    public float detectionDistance;
    public float minDistance;

    public float timeToChangeDirection;
    public float lateralMoveVel = 3;
    private bool indo = true;
    private Coroutine change;

    #region Lerp Stuff
    /*
    float y = 0.5f;
    float x;
    bool teste = true; // true == subindo valor, false == descendo valor
    */
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        hoi = this.GetComponent<NavMeshAgent>();
        change = StartCoroutine(ChangeDirection());
    }

    void Update()
    {
#if (UNITY_EDITOR)

        #region Lerp Stuff
        /*
         * Lerp test, this fucking bullshit works, somehow
        if(teste)
        {
            x = Mathf.Lerp(-1, 1, y);
            y += 0.1f * Time.deltaTime;

            if (x >= 1)
            {
                y = 0;
                teste = false;
            }
        }
        else
        {
            x = Mathf.Lerp(1, -1, y);
            y += 0.1f * Time.deltaTime;

            if (x <= -1)
            {
                y = 0;
                teste = true;
            }
        }
        Debug.Log(x + " , " + teste + " , " + y);
        */
        #endregion

        if(indo)
        {
            hoi.Move(new Vector3(lateralMoveVel, 0, 0) * Time.deltaTime);
        }
        else
        {
            hoi.Move(new Vector3(lateralMoveVel*-1, 0, 0) * Time.deltaTime);
        }
        
#endif

        if (Vector3.Distance(this.transform.position, player.transform.position) <= detectionDistance)
        {
            hoi.SetDestination(player.transform.position);

            following = true;
        }
        
        if(following)
        {
            if(Vector3.Distance(this.transform.position, player.transform.position) <= minDistance)
            {
                Ray t = new Ray(this.transform.position, (player.transform.position - this.transform.position));
                RaycastHit you;

                Debug.DrawRay(this.transform.position, (player.transform.position - this.transform.position), Color.green);

                Physics.Raycast(t, out you);
                if (you.transform.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Attacking");
                    hoi.isStopped = true;
                }
                else
                {
                    Debug.Log("Finding Angle");
                    hoi.isStopped = false;
                }
            }
            else
            {
                //follow player
                hoi.isStopped = false;
            }
        }
    }

    private IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(timeToChangeDirection);

        indo = !indo;

        change = StartCoroutine(ChangeDirection());
    }
}
