using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private bool gamePaused;

    private Canvas pauseCanvas;
    public PauseScreens pauseScreens;

    private void Awake()
    {
        pauseCanvas = GetComponent<Canvas>();

        if (!gamePaused)
        {
            pauseCanvas.enabled = false;
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
                PauseGame();
            else
                UnpauseGame();
        }
        if(gamePaused && Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseCanvas.enabled = true;
        gamePaused = true;

    }

    public void UnpauseGame()
    {
        pauseScreens.Unpause();
        Time.timeScale = 1;
        pauseCanvas.enabled = false;
        gamePaused = false;
 
    }
}
