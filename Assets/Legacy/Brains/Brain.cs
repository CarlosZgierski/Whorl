using UnityEngine;
using System.Collections.Generic;


abstract public class Brain : ScriptableObject
{
    [SerializeField]
    State startingState;
    public List<State> states = new List<State>();
    Thinker thinker;
    protected GameObject gameObject;
    protected Transform transform;

    public virtual void Initialize(Thinker x)
    {
        thinker = x;
        gameObject = x.gameObject;
        transform = x.transform;
    }

    abstract public void Think();
}
