using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormTimer : MonoBehaviour
{
    public static FormTimer Instance;

    bool timeIsOver = false;
    public float time = 360f;
    public GameObject questionGameObject;
    public bool done = false;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        questionGameObject.gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Timer(time));
        timeIsOver = false;
    }

    IEnumerator Timer(float x)
    {
        yield return new WaitForSeconds(x);
        timeIsOver = true;
    }

    public void StrugglerDeath()
    {
        if(timeIsOver && done == false)
        {
            Ask();
        }
    }

    public void Ask()
    {

        questionGameObject.SetActive(true);
    }

    void CloseQuestion()
    {
        questionGameObject.SetActive(false);
    }

    public void OpenForm()
    {
        Application.OpenURL("https://forms.gle/gTNjrC4ezgTJZ7aDA");
        done = true;
        CloseQuestion();
    }

    public void MoreTime()
    {
        timeIsOver = false;
        StartCoroutine(Timer(5f));
        CloseQuestion();
    }

}
