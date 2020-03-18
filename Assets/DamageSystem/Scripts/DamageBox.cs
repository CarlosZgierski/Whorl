using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;


public class DamageBox : MonoBehaviour
{


    public bool Animated = false;
#if UNITY_EDITOR
    [SerializeField]
    [TextArea]
    private string DeveloperComment = "Animated DamageBoxes can only damage a enemy one time by activation";
#endif
    List<HealthController> DamagedHealthControllersInThisActivation;
 

    [HideInInspector]
    public bool GetDamageFromStatsModule = false;
    [HideInInspector]
    public StatsModule StatsModule;

    [Space]
    public bool hitPlayer;
    public bool hitEnemies;
    public bool hitNeutral;
    [HideInInspector]
    public float damageReference;
    public DamageType damageType;
    public bool DestroyOnCollision = false;

    

    private AudioSource audioSource;
    public event Action<HealthController> OnHit = delegate { };

    [HideInInspector]
    public TimeFreezerReference TimeFreezer;
    [HideInInspector]
    public float timeFreezeDuration = 0;

    [HideInInspector]
    public ScreenShakerReference ScreenShaker;
    [HideInInspector]
    public float screenShakeDuration = 0;
    [HideInInspector]
    public float screenShakeAmount = 1;

    




    private void Start()
    {
        if(GetDamageFromStatsModule)
        {
            damageReference = StatsModule.Damage;
            StatsModule.OnDamageChange += SetDamageTo;
        }
        if(Animated)
        {
            DamagedHealthControllersInThisActivation = new List<HealthController>();
        } 


        audioSource = GetComponent<AudioSource>();
    }

    private void SetDamageTo(float x)
    {
        damageReference = x;
    }

    private void OnEnable()
    {
        if (Animated)
        {
            if(DamagedHealthControllersInThisActivation == null)
            {
                DamagedHealthControllersInThisActivation = new List<HealthController>();
            }
            DamagedHealthControllersInThisActivation.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            HealthController x = other.gameObject.GetComponent<HealthController>();
            if (HitIsValidForDamage(x) && !x.Invulnerability)
            {
                if (Animated)
                {
                    if (!DamagedHealthControllersInThisActivation.Contains(x))
                    {
                        Hit(x);
                        DamagedHealthControllersInThisActivation.Add(x);
                    }
                }
                else
                {
                    Hit(x);
                }
                 
                
            }
        }
    }

    private bool HitIsValidForDamage(HealthController x)
    {
        return ((hitEnemies && x.entityType == EntityTypes.Enemy) || (
                hitPlayer && x.entityType == EntityTypes.Player) || (
                hitNeutral && x.entityType == EntityTypes.Neutral));
    }




    private void Hit(HealthController x)
    {

        
        x.DoDamage(damageReference, damageType,transform.position);
        OnHit(x);

        if (TimeFreezer)
        {
            TimeFreezer.FreezeTime(timeFreezeDuration);
        }

        if(ScreenShaker)
        {
            ScreenShaker.Shake(screenShakeDuration, screenShakeAmount);
        }

        if (DestroyOnCollision)
        {
            if (audioSource)
            {
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position, audioSource.volume);
            }
            Destroy(gameObject);
            
        }
        else
        {
            if(audioSource)
            {
                audioSource.Play();
            }
        }
    }


}


#region Editor
#if UNITY_EDITOR
[CustomEditor(typeof(DamageBox))]
public class DamageBoxEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as DamageBox;

        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        myScript.GetDamageFromStatsModule = GUILayout.Toggle(myScript.GetDamageFromStatsModule, "Get Damage From StatsModule");
        serializedObject.Update();

        if (myScript.GetDamageFromStatsModule)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("StatsModule"), true);
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
            if (EditorApplication.isPlaying)
            {
                GUILayout.Label("View only");
                GUI.enabled = false;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("damageReference"), true);
                GUI.enabled = true;
            }
            else
            {
                GUILayout.Label("You can view values on play");
            }
        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("damageReference"), true);
            serializedObject.ApplyModifiedProperties();
        }
        if(myScript.gameObject.GetComponent<AudioSource>())
        {
            GUILayout.Label("Audio Source detected");
        }
        else
        {
            GUILayout.Label("No Audio Source detected");
        }


        EditorGUILayout.PropertyField(serializedObject.FindProperty("TimeFreezer"), true);
        if (myScript.TimeFreezer != null)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("timeFreezeDuration"), true);
            
        }
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("ScreenShaker"), true);
        if (myScript.ScreenShaker != null)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("screenShakeDuration"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("screenShakeAmount"), true);

        }
        serializedObject.ApplyModifiedProperties();


        EditorGUILayout.Space();
        EditorGUILayout.Space();


        

    }

}
#endif
#endregion