using System.Collections.Generic;
using UnityEngine;

public class SkillSacrificeTrigger : MonoBehaviour
{
    [SerializeField] GameObjectVariable SkillSacrificeReference;
    [SerializeField] GameObjectVariable GameObjectReferenceToTrigger;
    [SerializeField] ListOfSkills ownedSkills;
 

 


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObjectReferenceToTrigger.Value && SkillSacrificeReference.Value)
        {
            SkillSacrificeDirector sacrificeDirector = SkillSacrificeReference.Value.GetComponent<SkillSacrificeDirector>();
            if (ownedSkills.list.Count > 0)
            {
                SkillSacrificeReference.Value.SetActive(true);
                Destroy(gameObject);
            }
        }
        }
    }


