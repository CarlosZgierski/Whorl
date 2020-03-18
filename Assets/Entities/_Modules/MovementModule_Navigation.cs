using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AI;

public class MovementModule_Navigation : MonoBehaviour
{
    public Transform target;

    //References
    private NavMeshAgent thisAgent;
    NavMeshBrain brain;
    AnimationModule_Enemy AnimationModule;
    NavMeshObstacle NavMeshObstacle;


    //Movememnt related Variables
    bool reachedMaxLateral = false;
    bool firstLateralRun = true;

    //Lateral Movement
    private bool going;
    private Coroutine change;

    

    [SerializeField] private bool makeLateralMov;
    [SerializeField] private float lateralMoveVel = 3;
    [SerializeField] private float timeToChangeDirection;

    public event Action OnMovementModuleEnabled = delegate { };
    public event Action OnMovementModuleDisabled = delegate { };

    // Start is called before the first frame update
    void Awake()
    {
        thisAgent = GetComponent<NavMeshAgent>();
        NavMeshObstacle = GetComponent<NavMeshObstacle>();
        if(NavMeshObstacle)
        {
            NavMeshObstacle.enabled = false;
        }
    }

    private void Start()
    {
        AnimationModule = GetComponent<AnimationModule_Enemy>();
        if(AnimationModule)
        {
            AnimationModule.OnHitAnimationStart += StopAgent;
            AnimationModule.OnHitAnimationEnd += UnstopAgent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            thisAgent.SetDestination(target.position);
            /* ISSO VAI SER USADO PARA AVIALIAR SE TEM UM CAMINHO NÃO BLOQUEADO PRA O PONTO
            NavMeshPath path = new NavMeshPath();
            thisAgent.CalculatePath(target.position, path);
            if (path.status == NavMeshPathStatus.PathPartial)
            {
                thisAgent.isStopped = true;
            }
            else
            {
                thisAgent.isStopped = false;

            }
            */
        }
    }

    public void EnableMovementToChaseTarget(Transform target)
    {
        if(NavMeshObstacle)
        {
            NavMeshObstacle.enabled = false;
        }
        StartCoroutine(wait(() =>
        {
            enabled = true;
            this.target = target;
            thisAgent.enabled = true;
            thisAgent.isStopped = false;
            thisAgent.SetDestination(target.transform.position);
            OnMovementModuleEnabled();
        }));
    }

    IEnumerator wait(Action x)
    {
        yield return new WaitForSeconds(0.01f);
        x();
    }


    public void DisableMovement()
    {
        if (NavMeshObstacle)
        {
            NavMeshObstacle.enabled = true;
        }
        enabled = false;
        thisAgent.enabled = true;
        target = null;
        thisAgent.isStopped = true;
        thisAgent.ResetPath();
        thisAgent.enabled = false;
        


        OnMovementModuleDisabled();
    }

    private void StopAgent()
    {
        if(thisAgent.enabled)
        thisAgent.isStopped = true;
    }

    private void UnstopAgent()
    {
        if (thisAgent.enabled)
            thisAgent.isStopped = false;

    }







    private IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(timeToChangeDirection);

        going = !going;

        change = StartCoroutine(ChangeDirection());
    }

    private void LateralMovement()
    {
        if (going)
        {
            thisAgent.Move(new Vector3(lateralMoveVel, 0, 0) * Time.deltaTime);
        }
        else
        {
            thisAgent.Move(new Vector3(lateralMoveVel * -1, 0, 0) * Time.deltaTime);
        }
    }
}
