
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ComposedButton : MonoBehaviour
{
    [SerializeField]
    Animator Animator;


    [SerializeField]
    float UnityEventDelayAfterClick = 0.5f;

    [SerializeField]
    UnityEvent UnityEventOnClick;

    

    public void Click()
    {
        SetClickedAnimationTrigger();
        StartCoroutine(CallUnityEventAfterDelay());
    }


    public IEnumerator CallUnityEventAfterDelay()
    {
        yield return new WaitForSecondsRealtime(UnityEventDelayAfterClick);
        UnityEventOnClick.Invoke();
    }

    public void SetMouseEnterAnimationTrigger()
    {
        Animator.SetTrigger("MouseEnter");
    }

    public void SetMouseExitAnimationTrigger()
    {
        Animator.SetTrigger("MouseExit");
    }

    public void SetClickedAnimationTrigger()
    {
        Animator.SetTrigger("Clicked");
    }




    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
