using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModule : MonoBehaviour
{
    public GameObjectVariable HealthBarGameObjectVariable;

    HealthController HealthController;
    HealthBar HealthBar;

    public GameEvent GameEventOnDeath;

    public bool SetHealthBarOnlyOnActivation = true;
 

    void Start()
    {
        HealthController = GetComponent<HealthController>();
        HealthController.OnDie += CallGameEvent;

        if (SetHealthBarOnlyOnActivation)
        {
            GetComponent<StateMachine>().OnActivation += SetBossHealthBar;
        }
        else
        {
            SetBossHealthBar();
        }
      
    }

    void SetBossHealthBar()
    {
        HealthBar = HealthBarGameObjectVariable.Value.GetComponent<HealthBar>();
        HealthBar.HealthController = HealthController;
        HealthBar.enabled = true;
        foreach (Transform x in HealthBar.transform.GetComponentsInChildren<Transform>(true))
        {
            x.gameObject.SetActive(true);
        }
    }

    void CallGameEvent(HealthController x)
    {
        GameEventOnDeath.Raise();
    }

  
}
