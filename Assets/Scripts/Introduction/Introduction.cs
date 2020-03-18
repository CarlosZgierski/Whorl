using UnityEngine.SceneManagement;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    static bool alreadyPlayed = false;
    public GameObjectVariable Struggler;
    
    void Awake()
    {
       
        if (alreadyPlayed)
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        FindObjectOfType<PauseManager>().enabled = false;
    }


    void DisableStruggler()
    {
        Struggler.Value.GetComponent<InputModule>().enabled = false;
        alreadyPlayed = true;

    }

    void EnableStruggler()
    {
        FindObjectOfType<PauseManager>().enabled = true;
        Struggler.Value.GetComponent<InputModule>().enabled = true;
    }

 
 


}
