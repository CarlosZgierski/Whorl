using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserBrain : StateMachine
{
    [SerializeField]
    GameObjectVariable TargetReference;

    MovementModule_Navigation MovementModule;
    AnimationModule_Enemy AnimationModule;
    HealthController HealthController;

    [SerializeField]
    float DistanceToStartChasing;
    [SerializeField]
    float DistanceToStopChasing;

    [SerializeField]
    TrailRenderer TrailRenderer;
    [SerializeField]
    Animator Animator;





    void Start()
    {
        MovementModule = GetComponent<MovementModule_Navigation>();
        AnimationModule = GetComponent<AnimationModule_Enemy>();
        HealthController = GetComponent<HealthController>();
        if (HealthController)
        {
            HealthController.OnDie += Die;
        }
        if (currentState == null)
            SetState(new ActivationState(this));

    }


    void Update()
    {
        currentState.Tick();
    }

    void Die(HealthController x)
    {
        gameObject.tag = "Untagged";
        SetState(new DeadState(this));
        AnimationModule.OnDeathAnimationEnd += DeathAnimationEndedEvent;
        Destroy(GetComponent<HealthController>());
        Destroy(GetComponent<DamageBox>());
        TrailRenderer.enabled = false;
        Destroy(transform.Find("Canvas").gameObject);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public override void Activate()
    {
        SetState(new ActivationState(this));

    }

    public override void Deactivate()
    {
        SetState(new InactiveState(this));
    }

    void DeathAnimationEndedEvent()
    {

        gameObject.tag = "Untagged";
        Destroy(GetComponent<Rigidbody>());
        AnimationModule.OnDeathAnimationEnd -= DeathAnimationEndedEvent;
        GetComponent<Collider>().isTrigger = true;
        

        //HealthController x = gameObject.AddComponent<HealthController>();
        //x.entityType = EntityTypes.Neutral;

        Destroy(this);
    }


    class InactiveState : State
    {
        ChaserBrain brain;

        public InactiveState(ChaserBrain brain)
        {
            this.brain = brain;
        }

        public override void Tick()
        {
            
        }

        public override void OnStateEnter()
        {
            if(!brain.AnimationModule)
            {
                brain.Start();
            }
            brain.AnimationModule.SetActiveBoolTo(false);
            brain.GetComponent<Collider>().isTrigger = false;
            brain.TrailRenderer.enabled = false;
            brain.GetComponent<HealthController>().enabled = false;
            //brain.OrbTrigger.OnActivation += ActivationTriggerEvent;
        }

        
    }

    class ActivationState : State
    {
        ChaserBrain brain;
        HealthController HealthController;

        float timer = 0;

        public ActivationState(ChaserBrain brain)
        {
            this.brain = brain;
        }

        public override void Tick()
        {
            timer += Time.deltaTime;
            if(timer > 2.3f)
            {
                ActivationEndAnimationEvent();   //MEDIDA DE SEGURANÇA PQ AS VEZES ELE NÂO ESTA FUNCIONANDO POR ANIMAÇÂO A ATIVAÇÂO AS VEZES
            }
        }

        public override void OnStateEnter()
        {
            HealthController = brain.GetComponent<HealthController>();
            if (HealthController)
            {
                brain.TrailRenderer.enabled = true;
                brain.AnimationModule.SetActiveBoolTo(true);
                brain.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                brain.AnimationModule.OnActivationAnimationEnd += ActivationEndAnimationEvent;
            }
            else
            {
                brain.SetState(new DeadState(brain));
            }

        }

        private void ActivationEndAnimationEvent()
        {
            

                brain.AnimationModule.OnActivationAnimationEnd -= ActivationEndAnimationEvent;
                HealthController.enabled = true;
                brain.GetComponent<Collider>().isTrigger = true;
                brain.AnimationModule.SetActiveBoolTo(true);
                brain.SetState(new IdleState(brain));
        }
    }



    class IdleState : State
    {
        ChaserBrain brain;
        Transform brainTransform;
        Transform targetTransform;
        float distanceToStartChasing;


        public IdleState(ChaserBrain brain)
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

    class ChaseState : State
    {
        ChaserBrain brain;
        Transform brainTransform;
        Transform targetTransform;
        float distanceToStopChasing;


        public ChaseState(ChaserBrain brain)
        {
            this.brain = brain;
            brainTransform = brain.transform;
            targetTransform = brain.TargetReference.Value.transform;
            distanceToStopChasing = brain.DistanceToStopChasing;
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
            if (Vector3.Distance(brainTransform.position, targetTransform.position) > distanceToStopChasing)
            {
                brain.SetState(new IdleState(brain));
            }
       
        }

    }

    class DeadState : State
    {
        public DeadState(ChaserBrain brain)
        {

        }

        public override void Tick()
        {

        }
    }
}
