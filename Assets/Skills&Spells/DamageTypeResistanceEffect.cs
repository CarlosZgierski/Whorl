
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skill/Effect/DamageTypeResistanceEffect")]
public class DamageTypeResistanceEffect : Effect
{
    [SerializeField]
    private DamageType DamageType;
    [SerializeField]
    private float ResistanceValue;

    override public void Apply(GameObject target)
    {
        HealthController x = target.GetComponent<HealthController>();
        foreach(DamageTypeResistance d in x.DamageTypeResistances)
        {
            if(d.DamageType == DamageType)
            {
                d.ResistanceValue += ResistanceValue;
                return;
            }
        }
        x.DamageTypeResistances.Add(new DamageTypeResistance(DamageType, ResistanceValue));

    }


    override public void Remove(GameObject target)
    {

        HealthController x = target.GetComponent<HealthController>();
        foreach (DamageTypeResistance d in x.DamageTypeResistances)
        {
            if (d.DamageType == DamageType)
            {
                d.ResistanceValue -= ResistanceValue;
                if(d.ResistanceValue == 0)
                {
                    x.DamageTypeResistances.Remove(d);
                }
                return;
            }
        }
        Debug.LogWarning("Problem applying " + DamageType.name + " to" + target.name);
        x.DamageTypeResistances.Add(new DamageTypeResistance(DamageType, ResistanceValue));
    }
}
