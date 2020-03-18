using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    public GameObject CheatLevelBuiler;
    public float SpeedFloatVariable;
    public float DamageFloatVariable;
    public GameObjectVariable PlayerReference;
    public GameObjectVariable SkillTreeReference;
    public DamageType damageType;


    public  void AddToSpeed(float add)
    {
        PlayerReference.Value.GetComponent<StatsModule>().AddValueToStat(Stats.Speed, add);
    }

    public void SetNoColliderTo(bool x)
    {
        PlayerReference.Value.GetComponent<Collider>().enabled = x;
    }

    public void DoDamageOnPlayer(float x)
    {
        PlayerReference.Value.GetComponent<HealthController>().DoDamage(x,damageType, null);
    }

    public void AddToDamage(float add)
    {
        PlayerReference.Value.GetComponent<StatsModule>().AddValueToStat(Stats.Damage,add);
    }

    public void TriggerSkillTreeCheatMode()
    {
        if (SkillTreeReference.Value)
        {
            SkillTreeReference.Value.GetComponent<SkillTree>().TriggerSkillCheat();
        }
    }

    public void AddToHealth(float x)
    {
        HealthController p = PlayerReference.Value.GetComponent<HealthController>();
        p.MaxHealth += x;
        p.health = p.MaxHealth;
        p.DoDamage(0, null, null);


    }

    public void EnableCheatLevel()
    { 


        FindObjectOfType<LevelBuilder>().gameObject.SetActive(false);
        foreach(Segment x in FindObjectsOfType<Segment>())
        {
            Destroy(x.gameObject);
        }
        CheatLevelBuiler.SetActive(true);
        
    }

    public void BlinkCheat()
    {
        if(GameObject.Find("BlinkAnimator"))
        GameObject.Find("BlinkAnimator").GetComponent<Animator>().SetTrigger("BlinkCheat");
    }



    public void GoBackToOldHealth()
    {
        HealthController p = PlayerReference.Value.GetComponent<HealthController>();
        StatsModule x = PlayerReference.Value.GetComponent<StatsModule>();
        p.MaxHealth = x.MaxHealth;
        p.health = x.MaxHealth;
        p.DoDamage(0, null, null);
    }

    public void GoToSegmentExit()
    {
        GameObject struggler = FindObjectOfType<SpellCaster_Struggler>().gameObject;
        foreach (Segment x in FindObjectsOfType<Segment>())
        {
            if(x.Exit.transform.position.z > struggler.transform.position.z)
            {
                struggler.GetComponent<Rigidbody>().MovePosition(x.Exit.transform.position);
            }
        }
         
    }



}
