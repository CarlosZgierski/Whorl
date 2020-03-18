using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;



public enum EntityTypes
{
    Player,
    Enemy,
    Neutral,
    Invulnerable
}

public class HealthController : MonoBehaviour {

    StatsModule StatsModule;

    public EntityTypes entityType;
    [HideInInspector]
    public float health;
    public float Health {
        get
        {
            return health;
        }
    }
    [HideInInspector]
    public float MaxHealth;


    public event Action<float, DamageType, HealthController> OnDamage = delegate { };
    public event Action<float, DamageType, HealthController, Vector3?> OnDamageWithPosition = delegate { };
    public event Action<HealthController> OnDie = delegate{ };

    public List<DamageTypeResistance> DamageTypeResistances = new List<DamageTypeResistance>();

    [Space]
    [SerializeField]
    bool DestroyOnDie = false;
    public GameEvent OnDeathSOEvent;

    [Header("Health Bar")]
    public GameObject healthBarPrefab;

    [HideInInspector]
    public ScreenShakerReference ScreenShaker;
    [HideInInspector]
    public float screenShakeDuration = 0;
    [HideInInspector]
    public float screenShakeAmount = 1;

    public bool Invulnerability = false;

    void Start()
    {

        StatsModule = GetComponent<StatsModule>();
        
        if (StatsModule)
        {
            MaxHealth = StatsModule.MaxHealth;
            StatsModule.OnMaxHealthChange += SetMaxHealthTo;
        }

        health = MaxHealth;
        gameObject.tag = "Health";

        if (healthBarPrefab != null)
        {
            CreateHealthBar();
        }   
        
    }


    private void OnDestroy()
    {
        gameObject.tag = "Untagged";
    }

    #region Public Methods

    public void Die()
    {
       

        if (entityType == EntityTypes.Player) //TESTE ----------------
            Time.timeScale = 1; //TESTE ----------------

        OnDie(this);

        if(OnDeathSOEvent)
        {
            OnDeathSOEvent.Raise();
        }
        if(DestroyOnDie)
        {
            Destroy(gameObject);
        }
    }

    public void DoDamage(float pureDamage, DamageType type, Vector3? positionOfDamageSource)
    {
        float damage = CalculateDamage(pureDamage, type);
        health -= damage;
        health = Mathf.Clamp(health, 0, MaxHealth);
        OnDamage(damage, type, this);
        OnDamageWithPosition(damage, type, this, positionOfDamageSource);
        if (ScreenShaker)
        {
            ScreenShaker.Shake(screenShakeDuration, screenShakeAmount);
        }

        if (health <= 0)
        {
            Die();
        }
        
    }

    public void SetInvulnerabilityTo(bool x)
    {
        Invulnerability = x;
    }

    #endregion



    #region Private Methods

    void SetMaxHealthTo(float x)
    {
        MaxHealth = x;
        health = Mathf.Clamp(Health, 0, MaxHealth);
    }


    float CalculateDamage(float pureDamage, DamageType type)
    {
        float totalResistance = 0;
        if (type)
        {
            foreach (DamageTypeResistance x in DamageTypeResistances)
            {
                if (type == x.DamageType)
                {
                    totalResistance += x.ResistanceValue;

                }
            }
        }
        return (1 - totalResistance) * pureDamage;
    }

    void CreateHealthBar()
    {
        Instantiate(healthBarPrefab, transform.position, Quaternion.identity).GetComponent<HealthBar>().Initialize(this);
    }



    #endregion



}

#region Editor
#if UNITY_EDITOR
[CustomEditor(typeof(HealthController))]
public class HealthControllerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as HealthController;

        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (myScript.gameObject.GetComponent<StatsModule>())
        {
            GUILayout.Label("StatsModule detected");
            if (EditorApplication.isPlaying)
            {
                GUILayout.Label("View only");
                GUI.enabled = false;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("MaxHealth"), true);
                GUI.enabled = true;
            }
            else
            {
                GUILayout.Label("You can view values on play");
            }
        }
        else
        {
            GUILayout.Label("StatsModule not detected");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("MaxHealth"), true);
            serializedObject.ApplyModifiedProperties();
        }



        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("health"), true);
        }
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ScreenShaker"), true);
        if (myScript.ScreenShaker != null)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("screenShakeDuration"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("screenShakeAmount"), true);

        }
        serializedObject.ApplyModifiedProperties();



    }

}

#endif
#endregion