
using UnityEngine;



public class TurretBrain : StateMachine
{
    [SerializeField]
    GameObjectVariable TargetReference;

    RotationModule_Enemy RotationModule;
    AttackModule_Enemy AttackModule;
    HealthController HealthController;

    [SerializeField]
    float DistanceToAim;
    [SerializeField]
    float DistanceToStopAim;

    [SerializeField]
    LayerMask AimLayerMask;


    void Start()
    {
        RotationModule = GetComponent<RotationModule_Enemy>();
        AttackModule = GetComponent<AttackModule_Enemy>();
        HealthController = GetComponent<HealthController>();
        HealthController.OnDie += Die;

        SetState(new IdleState(this));

    }

    private void Die(HealthController x)
    {
        SetState(new DeadState(this));
        Destroy(this);
        Destroy(HealthController);
        Destroy(RotationModule);
        Destroy(transform.GetChild(0).gameObject);
        Destroy(AttackModule);
        Destroy(GetComponent<SpellCaster>());



    }

    private void Update()
    {
        currentState.Tick();
    }

    #region States

    class IdleState : State
    {
        TurretBrain brain;
        Transform brainTransform;
        Transform targetTransform;
        float distanceToStartAim;

        public IdleState(TurretBrain brain)
        {
            this.brain = brain;
            targetTransform = brain.TargetReference.Value.transform;
            brainTransform = brain.transform;
            distanceToStartAim = brain.DistanceToAim;
        }

        public override void Tick()
        {
            if(Vector3.Distance(brainTransform.position, targetTransform.position) < distanceToStartAim)
            {
                brain.SetState(new AimState(brain));
            }
        
        }
    }

    class AimState : State
    {
        TurretBrain brain;
        Transform targetTransform;
        Transform brainTransform;
        float distanceToStopAim;
        GameObject target;

        public AimState(TurretBrain brain)
        {
            this.brain = brain;
            target = brain.TargetReference.Value;
            targetTransform = target.transform;
            targetTransform = brain.TargetReference.Value.transform;
            brainTransform = brain.transform;
            distanceToStopAim = brain.DistanceToStopAim;

        }

        public override void OnStateEnter()
        {
            
            brain.RotationModule.EnableRotationToFaceTarget(targetTransform);

        }

        public override void OnStateExit()
        {
            brain.RotationModule.DisableRotation();
        }

        private bool HasLineOfSightToAttackTarget()
        {
            //Como o modelo tem o pivot no chão estava pegando colisão com o chão
            Vector3 rayStartingPoint = brainTransform.position;
            Ray t = new Ray(rayStartingPoint, brainTransform.forward);
            RaycastHit you;

            if (Physics.Raycast(t, out you, brain.DistanceToAim,brain.AimLayerMask) && (you.collider.transform.gameObject == target))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Tick()
        {
            if(HasLineOfSightToAttackTarget())
            {
                brain.SetState(new AttackState(brain));
            }

            if (Vector3.Distance(brainTransform.position, targetTransform.position) > distanceToStopAim)
            {
                brain.SetState(new IdleState(brain));
            }


        }

        

    }

    class AttackState : State
    {
        TurretBrain brain;
        Transform brainTransform;
        GameObject target;

        public AttackState(TurretBrain brain)
        {
            this.brain = brain;
            brainTransform = brain.transform;
            target = brain.TargetReference.Value;

        }

        public override void OnStateEnter()
        {
            brain.AttackModule.EnableToAttack();
            brain.AttackModule.OnAttackEnd += EndAttack;

        }

        public override void Tick()
        {

        }

        private bool HasLineOfSightToAttackTarget()
        {
            //Como o modelo tem o pivot no chão estava pegando colisão com o chão
            Vector3 rayStartingPoint = brainTransform.position;
            Ray t = new Ray(rayStartingPoint, brainTransform.forward);
            RaycastHit you;

            if (Physics.Raycast(t , out you, brain.DistanceToAim, brain.AimLayerMask) && (you.collider.transform.gameObject == target))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void EndAttack()
        {
            brain.AttackModule.OnAttackEnd -= EndAttack;

            if (HasLineOfSightToAttackTarget())
            {
                brain.SetState(new AttackState(brain));
            }
            else
            {
                brain.SetState(new AimState(brain));
            }

        }



    }


    class DeadState : State
    {
        public DeadState(TurretBrain brain)
        {

        }

        public override void Tick()
        {

        }
    }


    #endregion
}
