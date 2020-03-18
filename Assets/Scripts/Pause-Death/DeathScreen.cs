using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public int GameScene = 1;
    private void Awake()
    {
        Time.timeScale = 0;

    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        //This works, takes a little too long though
        SceneManager.LoadScene(GameScene);
    }
}
