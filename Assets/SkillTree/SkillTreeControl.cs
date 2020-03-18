using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeControl : MonoBehaviour
{
    Vector3 oldPosition;
    private void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            oldPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            
            Vector3 amountToMove = Input.mousePosition - oldPosition;
            transform.Translate(amountToMove);
            oldPosition = Input.mousePosition;
        }
        float x = Input.GetAxis("Mouse ScrollWheel") * 2f;
        transform.localScale += new Vector3(x,x, 0);
        if(transform.localScale.x > 2.5f)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        }
        if(transform.localScale.x < 1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
