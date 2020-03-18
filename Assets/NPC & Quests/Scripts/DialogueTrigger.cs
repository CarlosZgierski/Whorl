using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    Coroutine CheckInputCoroutine;
    public CharacterApparition CharacterApparition;
    public bool dialogueBeingDisplayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            if(other.GetComponent<HealthController>().entityType == EntityTypes.Player)
            {
                CheckInputCoroutine = StartCoroutine(CheckInput());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            if (other.GetComponent<HealthController>().entityType == EntityTypes.Player)
            {
                StopCoroutine(CheckInputCoroutine);
            }
        }
    }

    IEnumerator CheckInput()
    {
        while(true)
        {
            if((Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) && dialogueBeingDisplayed == false)
            {
                if (Time.timeScale > 0)
                {
                    CharacterApparition.StartDisplayingFirstDialogueOnList();
                }
            }
            yield return null;
        }
    }
}
