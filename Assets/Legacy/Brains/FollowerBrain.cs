
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Brain/FollowerBrain")]
public class FollowerBrain : Brain
{
    //public string GameObjectNameToFollow;
    public GameObjectVariable GameObjectVariableToFollow;
    public float DistanceToKeepFromTarget = 2;
    public float DistanceToStartFollowin = 200;
    
    Transform target;
    Animator anim;//NÃO VAI CONTROLAR ANIMAÇÃO

    private bool spoted = false;
    

    public override void Initialize(Thinker x)
    {
        base.Initialize(x);
        //target = GameObject.Find(GameObjectNameToFollow).transform;
        target = GameObjectVariableToFollow.Value.transform;
        anim = x.GetComponent<Animator>();//NÃO VAI CONTROLAR ANIMAÇÃO
    }

    override public void Think()
    {
      
            MyFunctions.LookAtOnlyYAxis(transform, target.position);
            spoted = MyFunctions.Distance(transform.position, target.position) < DistanceToStartFollowin;

            //Debug.LogWarning(spoted);
            //if(Input.GetKeyDown(KeyCode.KeypadEnter)) Debug.LogWarning(MyFunctions.Distance(transform.position, target.position));

            if (spoted)
            {
                if (MyFunctions.Distance(transform.position, target.position) > DistanceToKeepFromTarget)
                {
                    anim.SetBool("Walking", true);//NÃO VAI CONTROLAR ANIMAÇÃO
                    anim.SetBool("Wrecking", false);
                    transform.Translate(0, 0, 1 * Time.deltaTime);
                }
                else
                {
                    anim.SetBool("Walking", false);//NÃO VAI CONTROLAR ANIMAÇÃO
                    anim.SetBool("Wrecking", true);
                }
            }
        
    }
}
