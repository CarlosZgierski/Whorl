using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/List/SkillList")]
public class ListOfSkills : ListOfObjects<Skill>
{


    #region Public Methods

    public List<Effect> GetListOfEffectsInSkills()
    {
        List<Effect> x = new List<Effect>();
        foreach (Skill i in list)
        {
            foreach(Effect o in i.Effects)
            {
                x.Add(o);
            }
        }
        return x;
    }

    public void CleanList()
    {
        list.Clear();
    }


    public void AddSkill(Skill x)
    {
        list.Add(x);

    }

    public void RemoveSkill(Skill x)
    {
        list.Remove(x);

    }

    #endregion











}


/*
#if UNITY_EDITOR
[CustomEditor(typeof(ListOfSkills))]
class ListOfSkillsHelperEditor : Editor
{
    ListOfSkills x;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        x = (ListOfSkills)target;
        Buttons();
        
      
   

    }

    void Buttons()
    {
        if (GUILayout.Button("Apply Effects"))
        {
            //x.ApplyOwnedSkillsEffects();
        }


        if (GUILayout.Button("Clean List and Remove Effects"))
        {
            //x.RemoveOwnedSkillsEffects();
        }
    }
}
#endif
*/