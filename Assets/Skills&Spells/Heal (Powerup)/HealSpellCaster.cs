using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpellCaster : MonoBehaviour
{
    [SerializeField]
    float HealAmount = 3f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        
       SpellInstance X = GetComponent<SpellInstance>();
       if(X.spellCaster.TryGetComponent(out HealthController x))
        {
            if (x.health < x.MaxHealth)
            {
                x.DoDamage(-HealAmount, null, transform.position);
                yield return new WaitForSeconds(2f);
                Destroy(gameObject);
            }
        }
           

    }

    
}
