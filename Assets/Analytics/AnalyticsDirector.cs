
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;

public class AnalyticsDirector : MonoBehaviour
{
    [SerializeField]
    bool WorkOnEditor = false;

    [SerializeField]
    ListOfSkills OwnedSkills;

    [SerializeField]
    LevelBuilder LevelBuilder;

    string playerID;

    private void Awake()
    {
        playerID = GetPlayerID();

        if (WorkOnEditor == false && Application.isEditor)
        {
            Destroy(gameObject);
        }
    }


    public void SkillsetChange()
    {
        var x = new Dictionary<string, object>();
        x.Add("ID", playerID);
        for (int i = 0; i < OwnedSkills.list.Count; i++)
        {
            x.Add("Skill Name (" + i + ")", OwnedSkills.list[i].name);
        }
        
        Analytics.CustomEvent("SkillSetChange", x);
    }

    private string GetPlayerID()
    {
        return AnalyticsSessionInfo.userId.Substring(0, 5);
    }


    public void PlayerDeath()
    {
        Analytics.CustomEvent("PlayerDied", new Dictionary<string, object>
        {
            {"ID", playerID},
            {"PlaythroughTime",Time.timeSinceLevelLoad },
            {"Segment", LevelBuilder.GetCurrentSegmentName() }

            
        });
    }

    public void LevelStart()
    {
        Analytics.CustomEvent("NewPlaythrough", new Dictionary<string, object>
        {
            {"ID", playerID},

        });
    }

    public void PlayerKilledBoss()
    {
        Analytics.CustomEvent("BossDefeated", new Dictionary<string, object>
        {
            {"ID", playerID},
            {"PlaythroughTime", Time.timeSinceLevelLoad }

        });
    }
}
