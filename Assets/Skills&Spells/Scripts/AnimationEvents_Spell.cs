
using UnityEngine;

[RequireComponent(typeof(SpellInstance))]
public class AnimationEvents_Spell : MonoBehaviour
{
    SpellInstance SpellInstance;


    private void Start()
    {
        SpellInstance = GetComponent<SpellInstance>();
    }

    #region AnimationEvents


    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void DisableTrailRenderersInChildren()
    {
        foreach(TrailRenderer x in GetComponentsInChildren<TrailRenderer>())
        {
            x.emitting = false;
        }
    }

    public void DisableMovement()
    {
        MovementModule x = SpellInstance.caster.GetComponent<MovementModule>();
        if (x)
        {
            x.enabled = false;
        }
    }

    public void EnableMovement()
    {
        MovementModule x = SpellInstance.caster.GetComponent<MovementModule>();
        if (x)
        {
            x.enabled = true;
        }
    }

    public void DisableRotation()
    {
        RotationModule x = SpellInstance.caster.GetComponent<RotationModule>();
        if (x)
        {
            x.enabled = false;
        }
    }

    public void EnableRotation()
    {
        RotationModule x = SpellInstance.caster.GetComponent<RotationModule>();
        if (x)
        {
            x.enabled = true;
        }
    }

    public void UnparentToCaster()
    {
        transform.parent = null;
    }

    public void ParentToCaster()
    {
        transform.parent = SpellInstance.caster.transform;
    }


    public void StartCastingAnimation()
    {
        SpellInstance.caster.GetComponent<AnimationModule>().SetCastingTo(true);
        SpellInstance.caster.GetComponent<MovementModule>().enabled = false;
        SpellInstance.caster.GetComponent<RotationModule>().enabled = false;
    }

    public void EndCastingAnimation()
    {
        SpellInstance.caster.GetComponent<AnimationModule>().SetCastingTo(false);
        SpellInstance.caster.GetComponent<MovementModule>().enabled = true;
        SpellInstance.caster.GetComponent<RotationModule>().enabled = true;
    }


    #endregion
}
