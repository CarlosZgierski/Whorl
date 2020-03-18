
using UnityEngine;
using System;

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;
    public virtual event Action OnActivation = delegate { };


  

    protected void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    public virtual void Activate()
    {
        OnActivation();
    }

    public void RaiseOnActivationEvent()
    {
        OnActivation();
    }

    public virtual void Deactivate()
    {

    }
}
