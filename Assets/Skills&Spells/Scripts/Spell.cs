
using UnityEngine;

abstract public class Spell : Effect
{
    [SerializeField]
    float cooldown;
    [SerializeField]
    Sprite iconSprite;
    [SerializeField]
    bool triggerCastTargetAnimation;

    public Sprite IconSprite
    {
        get
        {
            return iconSprite;
        }
    }

    public float Cooldown
    {
        get
        {
            return cooldown;
        }
    }

    public bool TriggerCastTargetAnimation
    {
        get
        {
            return triggerCastTargetAnimation;
        }
    }


    public override void Apply(GameObject target)
    {
        target.GetComponent<SpellCaster>().AddSpell(this);
       // throw new System.NotImplementedException();
    }

    public override void Remove(GameObject target)
    {
        target.GetComponent<SpellCaster>().RemoveSpell(this);
        //throw new System.NotImplementedException();
    }

    abstract public void Use(SpellCaster x);

 
}
