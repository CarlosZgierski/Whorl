using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class Encounter : MonoBehaviour
{

    public GameObject TriggersParent;
    public GameObject WallsParent;
    public GameObject EnemiesParent;

    bool encounterStarted = false;

    int enemiesToDie = 0;

    List<StateMachine> enemiesToActivate = new List<StateMachine>();

    private void Awake()
    {
        gameObject.SetActive(false);
        Invoke("SetGameObjectToTrue", 2f);
    }

    private void Start()
    {
        GetEnemiesToActivate();
        DeactivateAllEnemies();
        TriggersParent.SetActive(true);
        WallsParent.SetActive(false);
        SubscribeToEnemiesDeathEvent();
        
    }

    private void SetGameObjectToTrue()
    {
        gameObject.SetActive(true);
    }

    private void GetEnemiesToActivate()
    {
        foreach (StateMachine x in GetComponentsInChildren<StateMachine>())
        {
            enemiesToActivate.Add(x);
           
        }
    }

    private void DeactivateAllEnemies()
    {
        foreach (StateMachine x in enemiesToActivate)
        {
            x.Deactivate();
        }
    }

    private void ActivateAllEnemies()
    {
        foreach (StateMachine x in enemiesToActivate)
        {
            x.Activate();
        }
    }

    public void StartEncounter()
    {

        ActivateAllEnemies();
        DestroyAllTriggers();
        WallsParent.SetActive(true);
        
        FindObjectOfType<MusicController>().ChangeMusicToCombat();
        if (enemiesToActivate.Count == 0)
        {
            EndEncounter();
        }
        encounterStarted = true;
    }

    private void DestroyAllTriggers()
    {
        Destroy(TriggersParent);
    }

    private void SubscribeToEnemiesDeathEvent()
    {
        foreach(HealthController x in EnemiesParent.GetComponentsInChildren<HealthController>())
        {
            x.OnDie += EnemyDiedEvent;
            enemiesToDie++;
        }
    }

    private void EnemyDiedEvent(HealthController x)
    {
        enemiesToDie--;

        enemiesToActivate.Remove(x.gameObject.GetComponent<StateMachine>());

        if(enemiesToDie == 0 && encounterStarted)
        {
            EndEncounter();
        }
    }

    private void EndEncounter()
    {
        FindObjectOfType<MusicController>().ChangeMusicToExploration();
        Destroy(WallsParent);
        Destroy(this);
        gameObject.name = gameObject.name + " (Ended)";
    }
}

#region Editor
#if UNITY_EDITOR
[CustomEditor(typeof(Encounter))]
public class EncounterEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as Encounter;

        DrawDefaultInspector();
        if(GUILayout.Button("Show Only Triggers"))
        {
            myScript.TriggersParent.SetActive(true);
            myScript.WallsParent.SetActive(false);
        }
        if (GUILayout.Button("Show Only Walls"))
        {
            myScript.TriggersParent.SetActive(false);
            myScript.WallsParent.SetActive(true);
        }
        if (GUILayout.Button("Show Both"))
        {
            myScript.TriggersParent.SetActive(true);
            myScript.WallsParent.SetActive(true);
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        



    }
}

#endif
#endregion
