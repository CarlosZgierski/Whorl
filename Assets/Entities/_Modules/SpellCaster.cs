using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpellCaster : MonoBehaviour
{

    [SerializeField]
    protected List<Spell> OwnedSpells = new List<Spell>();

    public List<Spell> Spells { get { return OwnedSpells; } }

    public Transform InstantiatedSpellsParent;



    public virtual void AddSpell(Spell x)
    {
        OwnedSpells.Add(x);
    }

    public virtual void RemoveSpell(Spell x)
    {
        OwnedSpells.Remove(x);
    }

    public virtual void UseSpell(int spellIndex)
    {
        if(spellIndex < OwnedSpells.Count)
        {
            OwnedSpells[spellIndex].Use(this);
        }
    }

    public virtual void UseSpellOneTime(Spell x)
    {
        x.Use(this);
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(SpellCaster))]
public class SpellCasterEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as SpellCaster;
        serializedObject.Update();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("InstantiatedSpellsParent"), true);
        serializedObject.ApplyModifiedProperties();
        if (myScript.gameObject.GetComponent<EffectsModule>())
        {
            GUILayout.Label("EffectsModule detected");
            if (EditorApplication.isPlaying)
            {
                GUILayout.Label("View only");
                EditorGUILayout.PropertyField(serializedObject.FindProperty("OwnedSpells"), true);
            }
            else
            {
                GUILayout.Label("You can view values on play");
            }

        }
        else
        {
            GUILayout.Label("EffectsModule not detected");

            EditorGUILayout.PropertyField(serializedObject.FindProperty("OwnedSpells"), true);
            serializedObject.ApplyModifiedProperties();
        }
        if (myScript.gameObject.GetComponent<SpellCaster>() != myScript)
        {
            Debug.LogError("Multiple SpellCaster component on " + myScript.gameObject.name);
        }

        }
    }
#endif
