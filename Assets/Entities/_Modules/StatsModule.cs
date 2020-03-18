using System;
using UnityEngine;
using UnityEditor;

public class StatsModule : MonoBehaviour
{


    public bool UseScriptableObjectForStartingCharacterStats;
    [SerializeField]
    private CharacterStats StartingStatsFromCharacter;

    public bool StoreStatsInRunTimeStatsReference;
    public RuntimeStatsVariablesReference RuntimeStatsReference;

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float dodgeSpeed;


    public float MaxHealth { get { return maxHealth; } }
    public float Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float RotationSpeed { get { return rotationSpeed; } }
    public float DodgeSpeed { get { return dodgeSpeed; } }

    public event Action<float> OnMaxHealthChange = delegate { };
    public event Action<float> OnDamageChange = delegate { };
    public event Action<float> OnSpeedChange = delegate { };
    public event Action<float> OnRotationSpeedChange = delegate { };
    public event Action<float> OnDodgeSpeedChange = delegate { };


    public void Awake()
    {
        if(UseScriptableObjectForStartingCharacterStats)
        {
            maxHealth = StartingStatsFromCharacter.MaxHealth;
            damage = StartingStatsFromCharacter.Damage;
            speed= StartingStatsFromCharacter.Speed;
            rotationSpeed = StartingStatsFromCharacter.RotationSpeed;
            dodgeSpeed = StartingStatsFromCharacter.DodgeSpeed;

        }
        if(StoreStatsInRunTimeStatsReference)
        {
            RuntimeStatsReference.maxHealth = maxHealth;
            RuntimeStatsReference.damage = damage;
            RuntimeStatsReference.speed  = Speed;
            RuntimeStatsReference.rotationSpeed = rotationSpeed;
            RuntimeStatsReference.dodgeSpeed = dodgeSpeed;
        }
    }

    public void AddValueToStat(Stats StatToAdd, float ValueToAdd)
    {
        if(StatToAdd == Stats.MaxHealth)
        {
            maxHealth += ValueToAdd;
            OnMaxHealthChange(maxHealth);
        }
        else if (StatToAdd == Stats.Damage)
        {
            damage += ValueToAdd;
            OnDamageChange(damage);
        }
        else if (StatToAdd == Stats.Speed)
        {
            speed += ValueToAdd;
            OnSpeedChange(speed);

        }
        else if (StatToAdd == Stats.RotationSpeed)
        {
            rotationSpeed += ValueToAdd;
            OnRotationSpeedChange(rotationSpeed);
        }
        else
        {
            dodgeSpeed += ValueToAdd;
            OnDodgeSpeedChange(dodgeSpeed);
        }
    }


}





#region Editor
#if UNITY_EDITOR
[CustomEditor(typeof(StatsModule))]
public class StatsModuleEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as StatsModule;

        EditorGUILayout.Space();
        EditorGUILayout.Space();


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        myScript.UseScriptableObjectForStartingCharacterStats = GUILayout.Toggle(myScript.UseScriptableObjectForStartingCharacterStats, "Use Scriptable Object For Starting Character Stats");
        serializedObject.Update();

        if (myScript.UseScriptableObjectForStartingCharacterStats)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("StartingStatsFromCharacter"), true);
            serializedObject.ApplyModifiedProperties();
            if (EditorApplication.isPlaying)
            {
                EditorGUILayout.LabelField("View Only");
                GUI.enabled = false;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationSpeed"), true);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("dodgeSpeed"), true);
                GUI.enabled = true;
            }
            else
            {
                GUILayout.Label("You can view values on play");
            }

        }
        else
        {

            EditorGUILayout.LabelField("Edit");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationSpeed"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dodgeSpeed"), true);
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        myScript.StoreStatsInRunTimeStatsReference = GUILayout.Toggle(myScript.StoreStatsInRunTimeStatsReference, "Store Current Stats in Stats RunTime Reference");
        serializedObject.Update();
        if (myScript.StoreStatsInRunTimeStatsReference)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("RuntimeStatsReference"), true);
        }

    }

}

#endif
#endregion