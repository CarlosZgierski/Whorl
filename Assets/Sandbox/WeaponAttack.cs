using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private GameObject root;
    public WeaponMelee currentWeapon;

    private BoxCollider melee_hb;

    private void Start()
    {
        melee_hb = root.GetComponent<BoxCollider>();
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(MakeMeleeAtack());
        }
    }

    private IEnumerator MakeMeleeAtack()
    {
        melee_hb.size = currentWeapon.WeaponHitbox();
        melee_hb.center = currentWeapon.WeaponCenter();
        melee_hb.GetComponent<DamageBox>().damageReference = currentWeapon.Weapon_Damage();

        yield return new WaitForSeconds(currentWeapon.Weapon_Atack_Duration());
        melee_hb.size = Vector3.zero;
    }

    /*private IEnumerator MakeRangedAttack()
    {

    }
    */
}
