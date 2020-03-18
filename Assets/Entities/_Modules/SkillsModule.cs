using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(EffectsModule))]
public class SkillsModule : MonoBehaviour
{
    public bool  UseScriptableObjectForOwnedSkills = true;

    public ListOfSkills OwnedSkills;
    public List<Skill> ListOfSkills;
    private EffectsModule EffectsModule;

    [SerializeField]
    GameEvent OnSkillSetChangeGameEvent;



    private void Start()
    {

        EffectsModule = GetComponent<EffectsModule>();
        EffectsModule.ActiveEffects.Clear();

        if (UseScriptableObjectForOwnedSkills)
        {
            EffectsModule.ActiveEffects = OwnedSkills.GetListOfEffectsInSkills();
        }
        else
        {
            EffectsModule.ActiveEffects = GetListOfEffectsInListOfSkills(ListOfSkills);
        }
        EffectsModule.ApplyActiveEffects();
    }


    public void SetAndApplyEffectsFromOwnedSkills()
    {
        EffectsModule.CleanAndRemoveAllEffects();
        if (UseScriptableObjectForOwnedSkills)
        {
            EffectsModule.ActiveEffects = OwnedSkills.GetListOfEffectsInSkills();
        }
        else
        {
            EffectsModule.ActiveEffects = GetListOfEffectsInListOfSkills(ListOfSkills);
        }
        EffectsModule.ApplyActiveEffects();

    }



    private List<Effect> GetListOfEffectsInListOfSkills(List<Skill> y)
    {
        List<Effect> x = new List<Effect>();
        foreach (Skill i in y)
        {
            foreach (Effect o in i.Effects)
            {
                x.Add(o);
            }
        }
        return x;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(SkillsModule))]
public class  SkillsModuleEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as SkillsModule;
      
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        myScript.UseScriptableObjectForOwnedSkills = GUILayout.Toggle(myScript.UseScriptableObjectForOwnedSkills, "Use Scriptable Object For Owned Skills");
        serializedObject.Update();
        if (myScript.UseScriptableObjectForOwnedSkills)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OwnedSkills"), true);
            serializedObject.ApplyModifiedProperties();
            if (EditorApplication.isPlaying)
            {
                EditorGUILayout.LabelField("View Only");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("ListOfSkills"), true);
            }
            else
            {
                GUILayout.Label("You can view values on play");
            }

        }
        else
        {
            
            EditorGUILayout.LabelField("Edit");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ListOfSkills"), true);
            serializedObject.ApplyModifiedProperties();
        }

    }
}
#endif
