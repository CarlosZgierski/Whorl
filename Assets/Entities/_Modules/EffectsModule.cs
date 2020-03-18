
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EffectsModule : MonoBehaviour
{
  
    public List<Effect> ActiveEffects;
    public List<Effect> TemporaryEffects;



    void Start()
    {
        if(!GetComponent<SkillsModule>())
        {
            ApplyActiveEffects();
        }  
    }



    public void ApplyActiveEffects()
    {
        foreach (Effect x in ActiveEffects)
        {
            x.Apply(gameObject);
        }
    }

    public void CleanAndRemoveAllEffects()
    {
        foreach (Effect x in ActiveEffects)
        {
            x.Remove(gameObject);
        }
        ActiveEffects.Clear();
    }

    public void ApplyTemporaryEffect(Effect x)
    {
        TemporaryEffects.Add(x);
        x.Apply(gameObject);
    }

    public void RemoveTemporaryEffect(Effect x)
    {
        TemporaryEffects.Remove(x);
        x.Remove(gameObject);
    }
    
}

#if UNITY_EDITOR
[CustomEditor(typeof(EffectsModule))]
public class EffectsModuleEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as EffectsModule;

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        serializedObject.Update();
        
        if (myScript.gameObject.GetComponent<SkillsModule>())
        {
            
            GUILayout.Label("SkillsModule detected");
            if (EditorApplication.isPlaying)
            {
                GUILayout.Label("View only");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("ActiveEffects"), true);
            }
            else
            {
                GUILayout.Label("You can view values on play");
            }
        }
        else
        {
            GUILayout.Label("SkillsModule not detected");
            GUILayout.Label("Edit");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ActiveEffects"), true);
            serializedObject.ApplyModifiedProperties();
        }

        if (myScript.gameObject.GetComponent<SpellCaster>())
        {

            GUILayout.Label("SpellCaster found");
        }
        else
        {
            GUILayout.Label("SpellCaster not found");
        }

    }
}
#endif
