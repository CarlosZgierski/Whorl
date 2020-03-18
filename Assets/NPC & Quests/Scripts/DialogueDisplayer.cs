using TMPro;
using UnityEngine;
using System;

public class DialogueDisplayer : MonoBehaviour
{
    public GameObjectVariable GameObjectReferenceToSet;
    Dialogue DialogueBeingDisplayed;
    int currentLineIndex;
    public TextMeshProUGUI Text;
    public InputModule InputModuleToDisable;

    public event Action OnDialogueOver = delegate { };

    void Start()
    {
        GameObjectReferenceToSet.Value = gameObject;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            currentLineIndex++;
            if (currentLineIndex > DialogueBeingDisplayed.Lines.Length - 1)
            {
                EndDialogue();
            }
            else
            {
                DisplayLine();
            }
        }
            
    }

    public void StartDialogue(Dialogue x)
    {
        DialogueBeingDisplayed = x;
        currentLineIndex = 0;
        DisplayLine();
        gameObject.SetActive(true);
        InputModuleToDisable.StopInput();

    }

 

    private void DisplayLine()
    {
        
        Text.text = DialogueBeingDisplayed.Lines[currentLineIndex].text;

    }

    private void EndDialogue()
    {
        InputModuleToDisable.ResumeInput();
        DialogueBeingDisplayed = null;
        gameObject.SetActive(false);
        OnDialogueOver();
    }


   
}
