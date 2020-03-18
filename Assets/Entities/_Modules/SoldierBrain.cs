using UnityEngine.AI;
using UnityEngine;

public class SoldierBrain : StateMachine
{
    [SerializeField]
    bool activatedOnStart = false;

    [SerializeField]
    GameObjectVariable TargetReference;


    MovementModule_Navigation MovementModule;
    RotationModule_Enemy RotationModule;
    AttackModule_Enemy AttackModule;
    HealthController HealthController;


    [SerializeField]
    float DistanceToStartChasing;
    [SerializeField]
    float DistanceToStopChasing;
    [SerializeField]
    float DistanceToStartAiming;
    [SerializeField]
    float DistanceToStopAiming;

    [SerializeField]
    LayerMask AimLayerMask;


    protected void Start()
    {
  

        RotationModule = GetComponent<RotationModule_Enemy>();
        MovementModule = GetComponent<MovementModule_Navigation>();
        AttackModule = GetComponent<AttackModule_Enemy>();
        HealthController = GetComponent<HealthController>();
        if(HealthController)
        {
            HealthController.OnDie += Die;
        }


        if (currentState == null)
            SetState(new InativeState(this));
        if(activatedOnStart)
        {
            SetState(new IdleState(this));
        }


    }

    public override void Activate()
    {
        SetState(new IdleState(this));
        RaiseOnActivationEvent();
    }

    public override void Deactivate()
    {

        SetState(new InativeState(this));
    }

    private void Die(HealthController x)
    {
        SetState(new DeadState(this));
        Destroy(this);
        Destroy(HealthController);
        Destroy(MovementModule);
        Destroy(RotationModule);
        Destroy(AttackModule);

        Destroy(GetComponent<SoundModule>());
        Destroy(GetComponent<NavMeshAgent>());
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<AnimationModule_Enemy>());

        gameObject.tag = "Untagged";
        Destroy(transform.GetComponentInChildren<Canvas>().gameObject);
    }

    private void Update()
    {
        currentState.Tick();
    }


    #region States

    class IdleState : State
    {
        SoldierBrain brain;
        Transform brainTransform;
        Transform targetTransform;
        float distanceToStartChasing;


        public IdleState(SoldierBrain brain)
        {
            this.brain = brain;
            brainTransform = brain.transform;
            targetTransform = brain.TargetReference.Value.transform;
            distanceToStartChasing = brain.DistanceToStartChasing;

        }

        public override void Tick()
        {
            if (Vector3.Distance(brainTransform.position, targetTransform.position) < distanceToStartChasing)
            {
                brain.SetState(new ChaseState(brain));
            }
        }



    }

    class InativeState : State
    {
      


        public InativeState(SoldierBrain brain)
        {

        }

      

        public override void Tick()
        {
     
        }
        
    }


    class ChaseState : State
    {
        SoldierBrain brain;
        Transform brainTransform;
        Transform targetTransform;
        float distanceToStopChasing;
        float distanceToStartAiming;


        public ChaseState(SoldierBrain brain)
        {
            this.brain = brain;
            brainTransform = brain.transform;
            targetTransform = brain.TargetReference.Value.transform;
            distanceToStopChasing = brain.DistanceToStopChasing;
            distanceToStartAiming = brain.DistanceToStartAiming;
        }

        public override void OnStateEnter()
        {
            brain.MovementModule.EnableMovementToChaseTarget(targetTransform);
        }

        public override void OnStateExit()
        {
            brain.MovementModule.DisableMovement();
        }

        public override void Tick()
        {
            float distanceFromTarget = GetDistanceFromSoldierToTarget();
            if (distanceFromTarget > distanceToStopChasing)
            {
                brain.SetState(new IdleState(brain));
            }
            else if (distanceFromTarget < distanceToStartAiming)
            {
                brain.SetState(new AimState(brain));
            }
        }

        private float GetDistanceFromSoldierToTarget()
        {
            return Vector3.Distance(brain.transform.position, brain.TargetReference.Value.transform.position);
        }

    }

    class AimState : State
    {
        SoldierBrain brain;
        Transform brainTransform;
        Transform targetTransform;
        float distanceToStopAiming;
        GameObject target;



        public AimState(SoldierBrain brain)
        {
            this.brain = brain;
            brainTransform = brain.transform;
            targetTransform = brain.TargetReference.Value.transform;
            distanceToStopAiming = brain.DistanceToStopAiming;
            target = targetTransform.gameObject;
        }

        public override void OnStateEnter()
        {
            brain.RotationModule.EnableRotationToFaceTarget(targetTransform);
        }

        public override void OnStateExit()
        {
            brain.RotationModule.DisableRotation();
        }


        public override void Tick()
        {
            if (HasLineOfSightToAttackTarget(out float distance))
            {
                brain.SetState(new AttackState(brain));
            }
            else if(Vector3.Distance(brainTransform.position, targetTransform.position) > distanceToStopAiming)
            {
                brain.SetState(new ChaseState(brain));

            }
        }

        private bool HasLineOfSightToAttackTarget(out float distance)
        {
            //Como o modelo tem o pivot no chão estava pegando colisão com o chão
            Vector3 rayStartingPoint = brainTransform.position + new Vector3(0, 0.5f, 0);
            Ray t = new Ray(rayStartingPoint, brainTransform.forward);
            RaycastHit you;

            if (Physics.Raycast(t, out you, brain.DistanceToStartAiming, brain.AimLayerMask) && (you.collider.transform.gameObject == target))
            {
                distance = you.distance;
                return true;
            }
            else
            {
                distance = 0;
                return false;
            }

        }
    }


    class AttackState : State
    {
        SoldierBrain brain;
        Transform brainTransform;
        GameObject target;


        public AttackState(SoldierBrain brain)
        {
           
            this.brain = brain;
            brainTransform = brain.transform;
            target = brain.TargetReference.Value;

            if (brain)
            {
                brain.AttackModule.EnableToAttack();
                brain.AttackModule.OnAttackEnd += EndAttack;
            }
            else
            {
                brain.SetState(new DeadState(brain));
            }

        }

        

        public override void Tick()
        {

        }

        private bool HasLineOfSightToAttackTarget(out float distance)
        {
            //Como o modelo tem o pivot no chão estava pegando colisão com o chão
            Vector3 rayStartingPoint = brainTransform.position + new Vector3(0, 0.5f, 0);
            Ray t = new Ray(rayStartingPoint, brainTransform.forward);
            RaycastHit you;
            
            if (Physics.Raycast(t, out you, brain.DistanceToStartAiming, brain.AimLayerMask) && (you.collider.transform.gameObject == target))
            {
                distance = you.distance;
                return true;
            }
            else
            {
                distance = 0;
                return false;
            }
            
        }

        private void EndAttack()
        {
            brain.AttackModule.OnAttackEnd -= EndAttack;

            if (HasLineOfSightToAttackTarget(out float distance))
            {
                brain.SetState(new AttackState(brain));
       
                
            }
            else
            {
                if(brain)
                brain.SetState(new AimState(brain));
            }
 
        }



    }

    class DeadState : State
    {
        public DeadState(SoldierBrain brain)
        {

        }

        public override void Tick()
        {

        }
    }



    #endregion


}
