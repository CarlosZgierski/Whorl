using System;
using UnityEngine;

public class SkillTreeSkillSlot : MonoBehaviour
{
    [SerializeField]
    SkillInSkillTree selectedSkill;
    public SkillInSkillTree SelectedSkill { get {return selectedSkill; } }

    public event Action OnSkillSelect = delegate { };
    public event Action OnSkillRemove = delegate { };

    [SerializeField]
    AudioSource audioSource;

    public void SelectSkill(SkillInSkillTree x)
    {
        selectedSkill = x;
        OnSkillSelect();
        audioSource.Play();
    }



    public void RemoveSkill()
    {
        selectedSkill = null;
        OnSkillRemove();
    }
}
