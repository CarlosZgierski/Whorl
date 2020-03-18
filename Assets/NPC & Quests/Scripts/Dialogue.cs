using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObjects/NPC and Quest/Dialogue")]
public class Dialogue : ScriptableObject
{
    public Character Character;
    [HideInInspector]
    public List<Condition> Conditions;
    public PlayerHasSkillCondition[] SkillConditions;
    public CurrentSegmentCondition[] CurrentSegmentConditions;
    public Line[] Lines;
    public bool Looped = false;
    public UnityEvent DialogueEndResult;

    public bool CheckConditions()
    {
        Conditions = new List<Condition>();
        Conditions.AddRange(SkillConditions);
        Conditions.AddRange(CurrentSegmentConditions);

        bool x = false;
        if (Conditions.Count > 0)
        {
            foreach (Condition cond in Conditions)
            {
                if (cond.Check())
                {
                    x = true;
                    
                }
            }
        }
        else
        {
            x = true;
        }

        return x;
    }

    

    [Serializable]
    public class Line
    {
        public string text;
    }

    [Serializable]
    public class Condition
    {
        public virtual bool Check()
        {
            return true;
        }
    }

    [Serializable]
    public class CurrentSegmentCondition : Condition
    {
        public GameObject Segment;

        public override bool Check()
        {
            Debug.Log(Segment.name + " " + FindObjectOfType<LevelBuilder>().GetCurrentSegment().name);
                if(Segment.name == FindObjectOfType<LevelBuilder>().GetCurrentSegment().name)
                {
                    return true;
                }
  
            return false;
        }
    }



    [Serializable]
    public class PlayerHasSkillCondition : Condition
    {
        public Skill SkillToCheck;

        public override bool Check()
        {

            foreach(Skill x in FindObjectOfType<SkillsModule>().OwnedSkills.list)
            {

                if(SkillToCheck == x)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

