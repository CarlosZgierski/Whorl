using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skill/Skill")]
public class Skill : ScriptableObject
{
    [SerializeField]
    private Skill requirement;
    [Space]
    [SerializeField]
    private new string name;

    [SerializeField]
    [TextArea]
    private string description;
    [SerializeField]
    private Sprite spriteInSkillTree;
    [SerializeField]
    private Sprite spriteInHUD;
    [Space]
    [SerializeField]
    private Effect[] effects;

    public Skill Requirement
    {
        get
        {
            return requirement;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    public Sprite SpriteInSkillTree
    {
        get
        {
            return spriteInSkillTree;
        }
    }

    public Sprite SpriteInHUD
    {
        get
        {
            return spriteInHUD;
        }
    }

    public Effect[] Effects
    {
        get
        {
            return effects;
        }
    }
        
}
