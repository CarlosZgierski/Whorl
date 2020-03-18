
using UnityEngine;

public class StateSO : ScriptableObject
{

    public Brain brain;

    public StateSO(Brain brain)
    {
        this.brain = brain;
    }

    public virtual void Tick()
    {

    }

    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateExit()
    {

    }
}
