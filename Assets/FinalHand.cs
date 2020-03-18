using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalHand : MonoBehaviour
{
    [SerializeField]
    ScreenShakerReference screenShaker;
    [SerializeField]
    GameObject barrierGameObject;
    [SerializeField]
    ParticleSystem particles;

    void PlayerEntered(Transform playerTransform)
    {
        StartCoroutine(TakeUp(playerTransform));
        
    }

    private IEnumerator TakeUp(Transform playerTransform)
    {

        particles.Stop();
        StartCoroutine(AfterTimeBackToMenu());
        barrierGameObject.SetActive(true);
        screenShaker.value.ShakeCamera(4, 2f);
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<FollowTargetPosition>().enabled = false;
        GameObject.Find("ShadowDrawer").transform.parent = null;
        while (true)
        {
            transform.Translate(0, 2 * Time.deltaTime, 0);
            playerTransform.Translate(0, 2 * Time.deltaTime, 0);
            
           
            yield return null;
        }
    }

    private IEnumerator AfterTimeBackToMenu()
    {
       
        yield return new WaitForSeconds(7.5f);
        FormTimer x = FindObjectOfType<FormTimer>();
        if (x)
        {
            x.OpenForm();
        }
      
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Health"))
        {
            if(other.gameObject.TryGetComponent(out HealthController x))
            {
                if(x.entityType == EntityTypes.Player)
                {
                    PlayerEntered(other.transform);
                }
            }
              
        }
    }
}
