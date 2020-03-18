using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(HealthController))]

public class NavMeshBrain : MonoBehaviour
{

    //Distance Variables
    [SerializeField] private float attackDistance;
    [SerializeField] private float chaseDistance;

    //References
    AnimationEvents_Enemy animationEvents;
    private GameObject playerRef;
    [SerializeField]
    private GameObjectVariable playerRefVariable;


    private bool chasingPlayer = false;
    private bool PlayerOnAttackDistance = false;
    private bool attacking = false;

    //HealthController Stuff
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private GameObject damageParticle;
    private HealthController hpController;


 
    public Action<GameObject> OnTargetLost = delegate { };
    public Action<GameObject> OnStartChasingTarget = delegate { };
    public Action<GameObject> OnTargetOnAttackRange = delegate { };
    public Action OnAttack = delegate { };
   





    //Animation & attack
    //[SerializeField] private Animator animator;

    void Start()
    {
        animationEvents = GetComponent<AnimationEvents_Enemy>();
        if (animationEvents)
        {

            animationEvents.OnAttackEnded += AttackAnimationEnded;
        }

        playerRef = playerRefVariable.Value;

        hpController = GetComponent<HealthController>();
        hpController.OnDie += Die;
    }

   


    void Update()
    {
        if(TargetIsOnChaseDistance())
        {
            //The player has reached the limit to be detected
            if (!chasingPlayer && !attacking)
            {
                StartChasingPlayer();
            }
            
        }
        else if (TargetIsOnAttackDistance())
        {
            if (PlayerOnAttackDistance == false)
            {
                PlayerOnAttackDistance = true;
                OnTargetOnAttackRange(playerRef);
                StopChasingPlayer();
                attacking = false;
            }
   
            if (HasLineOfSightToAttackTarget())
            {

                if (!attacking)
                {
                    AttackTarget();
                }
            }
            else
            {

            }
        }
        else
        {
            if (chasingPlayer)
            {
                StopChasingPlayer();
            }
        }

      
        
    }

    #region Private Functions
    private void AttackAnimationEnded()
    {
        OnTargetOnAttackRange(playerRef);
        attacking = false;
        chasingPlayer = false;
    }

    private void AttackTarget()
    {
        if (animationEvents)
        {
            attacking = true;
            OnAttack();
        }
        //animator.SetTrigger("Attack");
    }

    private void StartChasingPlayer()
    {
            OnStartChasingTarget(playerRef);
            chasingPlayer = true;
         PlayerOnAttackDistance = false;
    }

    private void StopChasingPlayer()
    {
        OnTargetLost(playerRef);
     
        chasingPlayer = false;
        
    }

    private bool TargetIsOnChaseDistance()
    {
        //Distance check between player and enemy
        if (playerRef)
        {
            float x = Vector3.Distance(transform.position, playerRef.transform.position);
            return (x <= chaseDistance && x > attackDistance);
        }
        else
            return false;
    }

   
    private bool TargetIsOnAttackDistance()
    {
        return (Vector3.Distance(transform.position, playerRef.transform.position) <= attackDistance);
    }

    private bool HasLineOfSightToAttackTarget()
    {
      
        Vector3 rayStartingPoint = transform.position + new Vector3(0, 0.5f, 0); //Como o modelo tem o pivot no chão estava pegando colisão com o chão
                                                                                 //Ray t = new Ray(rayStartingPoint, (playerRef.transform.position - this.transform.position));
        Ray t = new Ray(rayStartingPoint, transform.forward);
        RaycastHit you;

 
        if (Physics.Raycast(t, out you))
        {

            //////////////////////////
            /// Raycast feedbacks
            /// 
            if (you.transform.gameObject == playerRef) //Found a LoS to the Player
            {
                return true;

            }
            else //Didn't found a LoS to the Player
            {
                return false;
                //Debug.Log("Finding Angle");
                //thisAgent.isStopped = false;
            }
        }
        else
        {
            return false;
        }
    }






    private void Die(HealthController x)
    {
        if(deathParticle)
        Instantiate(deathParticle, transform.position, transform.rotation);
        //animator.SetTrigger("Dead");
        Destroy(this);
        Destroy(hpController);
        Destroy(GetComponent<MovementModule_Navigation>());
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(GetComponent<RotationModule_Enemy>());
        if (!GetComponent<Orb>())
        {
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<Rigidbody>());
        }
        Destroy(GetComponent<AnimationEvents_Enemy>());
        Destroy(GetComponent<AnimationModule_Enemy>());

        gameObject.tag = "Untagged";
        Destroy(transform.Find("Canvas").gameObject);
        
        

    }

  

    #endregion

    #region Public Functions

    #endregion
}
