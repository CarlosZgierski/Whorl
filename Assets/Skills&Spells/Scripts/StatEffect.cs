
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skill/Effect/StatEffect")]
public class StatEffect : Effect
{


    [SerializeField]
    private Stats Stat;
    [SerializeField]
    private float ValueToAdd;

    override public void Apply(GameObject target)
    {
        StatsModule x = target.GetComponent<StatsModule>();
        if(x)
        {
            x.AddValueToStat(Stat, ValueToAdd);
        }
 
    }

    override public void Remove(GameObject target)
    {
        StatsModule x = target.GetComponent<StatsModule>();
        if (x)
        {
            x.AddValueToStat(Stat, -ValueToAdd);
        }

    }


}
