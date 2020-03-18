using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreens : MonoBehaviour
{
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject mainPause;

    private void Awake()
    {
        optionsScreen.SetActive(false);
        mainPause.SetActive(true);
    }

    public void GoOptions()
    {
        optionsScreen.SetActive(true);
        mainPause.SetActive(false);
    }

    public void GoMainPause()
    {
        optionsScreen.SetActive(false);
        mainPause.SetActive(true);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Unpause()
    {
        optionsScreen.SetActive(false);
        mainPause.SetActive(true);
    }

}
