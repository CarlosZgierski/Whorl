using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
   
    public HealthController HealthController;
    bool showingHealthBar = false;
    Slider Slider;
    float verticalOffset = 0;
    public Animator Animator;


    public void Initialize(HealthController healthController)
    {
        HealthController = healthController;
        transform.SetParent(healthController.transform.Find("Canvas").transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;


        if (!GetComponent<HealthFade>())
        {
            gameObject.AddComponent(typeof(HealthFade));
        }
    }

    private IEnumerator Start()
    {
        yield return null;

        Slider = GetComponent<Slider>();
        
        HealthController.OnDamage += Refresh;
        HealthController.OnDie += DestroyThis;

        Slider.maxValue = HealthController.MaxHealth;
        Slider.value = HealthController.health;
    }


    private void DestroyThis(HealthController X)
    {
        Destroy(gameObject);
    }

    void Refresh(float damage, DamageType type, HealthController x)
    {
        
        Slider.value = HealthController.health;
        if(Animator)
        Animator.SetTrigger("Flash");
    }

}
