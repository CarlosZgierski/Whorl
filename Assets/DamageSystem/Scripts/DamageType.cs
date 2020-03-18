
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DamageType")]
public class DamageType : ScriptableObject
{
    [SerializeField]
    GameObject damageParticlePrefab;
    [SerializeField]
    GameObject healParticlePrefab;
    [SerializeField]
    GameObject zeroParticlePrefab;

    public GameObject DamageParticlePrefab
    {
        get
        {
            return damageParticlePrefab;
        }
    }

    public GameObject HealParticlePrefab
    {
        get
        {
            return healParticlePrefab;
        }
    }

    public GameObject ZeroParticlePrefab
    {
        get
        {
            return zeroParticlePrefab;
        }
    }

    public GameObject GetDamageParticlePrefabByDamage(float damage)
    {
        if(damage > 0)
        {
            return damageParticlePrefab;
        }
        else if(damage == 0)
        {
            return zeroParticlePrefab;
        }
        else
        {
            return healParticlePrefab;
        }
    }

}
