using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    StringVariable sceneNameToLoad;

    public void LoadScene(int x)
    {
        SceneManager.LoadScene(x);
    }

    public void LoadSceneWithLoading(string sceneName)
    {
        sceneNameToLoad.Value = sceneName;
        SceneManager.LoadScene(1);
        
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
