using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ScriptableObjects/NPC and Quest/QuestKeeper")]
public class QuestKeeper : ScriptableObject
{
    public QuestKeeper Value;
    public List<QuestWrapper> ActiveQuests = new List<QuestWrapper>();

    public void AddQuestToQuestKeeper(Quest quest)
    {
        Value.AddQuest(quest);
    }

    public bool ContainQuest(Quest x)
    {
        foreach (QuestWrapper questWrapper in ActiveQuests)
        {
            if (questWrapper.Quest == x)
            {
                return true;
            }
        }
        return false;
    }

    public void AddQuest(Quest x)
    {
        ActiveQuests.Add(new QuestWrapper(x));
    }

    public void NextQuestStage(Quest quest)
    {
        for (int i = 0; i < ActiveQuests.Count; i++)
        {
            if (ActiveQuests[i].Quest == quest)
            {
                ActiveQuests[i].NextQuestStage();
            }
        }
    }

    public List<Dialogue> GetListOfDialoguesWithCharacterInActiveQuests(Character x)
    {
        List<Dialogue> List = new List<Dialogue>();

        foreach (QuestWrapper questWrapper in ActiveQuests)
        {
            if (!questWrapper.complete)
            {
                foreach (Dialogue dialogue in questWrapper.Quest.Stages[questWrapper.currentStageIndex].Dialogues)
                {
                    if (dialogue.Character == x)
                    {
                        List.Add(dialogue);
                    }
                }
            }
        }
        return List;
    }

    [Serializable]
    public class QuestWrapper
    {
        public Quest Quest;
        public int currentStageIndex = 0;
        public bool complete = false;

        public QuestWrapper(Quest quest)
        {
            Quest = quest;
        }

        public void NextQuestStage()
        {
            currentStageIndex++;
            if (currentStageIndex >= Quest.Stages.Length)
            {
                EndQuest();
            }
        }

        public void EndQuest()
        {
            complete = true;
        }


    }

}
