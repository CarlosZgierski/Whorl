using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "ScriptableObjects/Weapons/Melee Weapon", order = 1)]
public class WeaponMelee : ScriptableObject
{
    public string weapon_name;

    //_hb == Hitbox size in Unity's units
    [SerializeField] private float weapon_reach_hb; //x
    [SerializeField] private float weapon_height_hb; //y
    [SerializeField] private float weapon_tickness_hb; //z

    [SerializeField] private float weapon_dmg;
    [SerializeField] private bool[] weapon_effects = new bool [5]; //effect_0==stun; effect_1==freeze; effect_2== burn; etc...
    [SerializeField] private float duration_of_atk; //in seconds


    public Vector3 WeaponHitbox()
    {
        return new Vector3(weapon_reach_hb, weapon_height_hb, weapon_tickness_hb);
    }

    public Vector3 WeaponCenter()
    {
        return new Vector3(weapon_reach_hb / 2, 0, 0);
    }

    public float Weapon_Atack_Duration()
    {
        return duration_of_atk;
    }

    public float Weapon_Damage()
    {
        return weapon_dmg;
    }

    public bool [] Weapon_Effects()
    {
        return weapon_effects;
    }
}