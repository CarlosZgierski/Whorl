using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float xLocalPositionToReset;
    [SerializeField]
    float resetXLocalPosition;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        if(transform.localPosition.x >= xLocalPositionToReset)
        {
            gameObject.SetActive(false);
            transform.localPosition = new Vector3(resetXLocalPosition, transform.position.y, transform.position.z);
            gameObject.SetActive(true);
        }
    }
}
