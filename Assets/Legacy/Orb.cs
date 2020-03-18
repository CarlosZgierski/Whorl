using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    Animator animator;
    Rigidbody Rigidbody;
    
    
    void Start()
    {
        
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();    
    }



    public void ActivateEnemy()
    {
        GetComponent<TrailRenderer>().enabled = true;
        animator.SetBool("Active", true);
        Rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
    }


    public void FinishedActivationAnimationEvent()
    {
       
    }

 


}
