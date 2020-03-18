using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] bool isFirst = false;
    static bool FirstTime = true;
    public static bool appeared = false;    

    [SerializeField]
    GameObject Trigger;
    [SerializeField]
    string[] lines;

    [SerializeField]
    Animator Animator;

    [SerializeField]
    GameObjectVariable DialogueDisplayerRef;
    DialogueDisplayer DialogueDisplayer;

    


    private void Start()
    {


        if (!FirstTime)
        {
            if (isFirst)
            {
                appeared = false;
                Destroy(gameObject);

            }
            else
            {
                if (appeared)
                {
                    Destroy(gameObject);
                }
                else
                {
                    appeared = true;
                }
            }
        }
        else
        {
            appeared = true;
            FirstTime = false;
        }
      
    }

    /*
    public void TriggerDialogue()
    {
        DialogueDisplayer = DialogueDisplayerRef.Value.GetComponent<DialogueDisplayer>();
        FirstTime = false;
        DialogueDisplayer.gameObject.SetActive(true);
        for (int i = 0; i < lines.Length + 1; i++)
        {
            if (i == 0)
            {
                Animator.SetTrigger("Reaction");
                DialogueDisplayer.DisplayText(lines[0]);
            }
            else
            {
                if (i < lines.Length)
                    StartCoroutine(DisplayDialogue(lines[i], i * 4));
                else
                {
                    StartCoroutine(EndDialogue(i * 4));
                }
            }
        }
    }


    IEnumerator DisplayDialogue(string x, int time)
    {
        yield return new WaitForSeconds(time);

        DialogueDisplayer.DisplayText(x);
    }

    IEnumerator EndDialogue (int time)
    {
        yield return new WaitForSeconds(time);
        DialogueDisplayer.gameObject.SetActive(false);
        Destroy(Trigger);
    }
    */
}
