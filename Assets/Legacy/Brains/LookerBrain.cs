using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Brain/LookerBrain")]
public class LookerBrain : Brain
{
    public string GameObjectNameToFollow;
    Transform target;
    Animator anim;

    public override void Initialize(Thinker x)
    {
        base.Initialize(x);
        target = GameObject.Find(GameObjectNameToFollow).transform;
        anim = x.GetComponent<Animator>();//NÃO VAI CONTROLAR ANIMAÇÃO
        anim.SetBool("Walking", false);
    }

    override public void Think()
    {
        MyFunctions.LookAtOnlyYAxis(transform, target.position);
    }
}
