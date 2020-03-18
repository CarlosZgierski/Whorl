using UnityEngine;

[CreateAssetMenu(fileName = "Ranged", menuName = "ScriptableObjects/Weapons/Ranged Weapon", order = 1)]
public class WeaponRanged : ScriptableObject
{
    public string weapon_name;

    //_hb == Hitbox size in Unity's units
    [SerializeField] private float weapon_reach_hb; //x
    [SerializeField] private float weapon_height_hb; //y
    [SerializeField] private float weapon_tickness_hb; //z

    [SerializeField] private bool piercing_projectile;
    [SerializeField] private float number_of_piercings; //the number of "bodies" the projectile can pierce

    [SerializeField] private float projectile_force;

    [SerializeField] private float weapon_dmg;
    [SerializeField] private bool[] weapon_effects = new bool[5];//effect_0==stun; effect_1==freeze; effect_2== burn; etc...
    [SerializeField] private float duration_of_flight; //in seconds

    public Vector3 WeaponHitbox()
    {
        return new Vector3(weapon_reach_hb, weapon_height_hb, weapon_tickness_hb);
    }

    public Vector3 WeaponCenter()
    {
        return new Vector3(weapon_reach_hb / 2, 0, 0);
    }

    public float ProjectileForce()
    {
        return projectile_force;
    }

    public float Weapon_Time_to_Fade()
    {
        return duration_of_flight;
    }

    public float WeaponDamage()
    {
        return weapon_dmg;
    }

    public bool[] WeaponEffects()
    {
        return weapon_effects;
    }
}
