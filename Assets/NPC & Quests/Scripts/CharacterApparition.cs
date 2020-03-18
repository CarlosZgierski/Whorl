using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterApparition : MonoBehaviour
{
    public QuestKeeper QuestKeeper;
    public Character Character;
    public GameObjectVariable DialogueDisplayerReference;
    public DialogueTrigger DialogueTrigger;

    bool DialoguesSeted = false;
    

    List<Dialogue> Dialogues = new List<Dialogue>();


    private void SetDialogues()
    {
        if (Character.IntroductionQuests.Length > 0)
        {
            AddIntroductionQuests();
        }

        Dialogues.AddRange(GetDialoguesInActiveQuests());
        Dialogues.AddRange(GetDialoguesFromCharacter());
        DialoguesSeted = true;


    }



    private void AddIntroductionQuests()
    {
        foreach (Quest x in Character.IntroductionQuests)
        {
            if (!QuestKeeper.ContainQuest(x))
            {
                QuestKeeper.AddQuest(x);
            }

        }
    }

    public void StartDisplayingFirstDialogueOnList()
    {
        if(!DialoguesSeted)
        {
            SetDialogues();
        }
        DialogueTrigger.dialogueBeingDisplayed = true;
        DialogueDisplayer displayer = DialogueDisplayerReference.Value.GetComponent<DialogueDisplayer>();
        displayer.StartDialogue(Dialogues[0]);
        displayer.OnDialogueOver += DisplayDialogueEndEvent;
        
    }

    public void DisplayDialogueEndEvent()
    {
        StartCoroutine(TurnDialogueBeingDisplayedBoolInDialogueTriggerFalse());
        DialogueDisplayerReference.Value.GetComponent<DialogueDisplayer>().OnDialogueOver -= DisplayDialogueEndEvent;
        Dialogues[0].DialogueEndResult.Invoke();
        if (!Dialogues[0].Looped)
        {
            Dialogues.Remove(Dialogues[0]);
        }
    }

    IEnumerator TurnDialogueBeingDisplayedBoolInDialogueTriggerFalse()
    {
        yield return new WaitForSeconds(0.4f);
        DialogueTrigger.dialogueBeingDisplayed = false;
    }

    public void OnDisable()
    {
        Character.instantiated = false;
    }

    private List<Dialogue> GetDialoguesInActiveQuests()
    {
        var FoundDialogues = QuestKeeper.GetListOfDialoguesWithCharacterInActiveQuests(Character);
        for(int i = FoundDialogues.Count - 1; i >= 0; i--)
        { 
            if(!FoundDialogues[i].CheckConditions())
            {
                FoundDialogues.Remove(FoundDialogues[i]);
            }
        }


        return FoundDialogues;
    }

    private List<Dialogue> GetDialoguesFromCharacter()
    {
        List<Dialogue> Result = new List<Dialogue>();
        

        foreach(Dialogue x in Character.observationDialogues)
        {
            if (x.CheckConditions())
            {
                Result.Add(x);
            }

        }
        return Result;
    }


}
