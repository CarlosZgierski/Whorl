using UnityEngine;
using System;
using UnityEngine.UI;

public class FollowTargetPosition : MonoBehaviour
{
    public Transform target;
    public float safeZoneAdjust;
    public float cameraLerpSpeed = 1f;

    Vector3 offset;

    private void Start()
    {
        offset = target.position - transform.position;
    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 desiredPosition;

            if (MouseOffSafeZone()) //Só vai mexer a camera se tiver fora da SafeZone delimitada
            {
                desiredPosition = target.position - offset;
            }
            else //Camera esta na SafeZone
            {
                desiredPosition = target.parent.transform.position - new Vector3(0,0,safeZoneAdjust);
            }

            desiredPosition = RoundDesiredPosition(desiredPosition);

            transform.position = Vector3.Lerp(transform.position,desiredPosition, Time.deltaTime * cameraLerpSpeed);
        }
    }

    Vector3 RoundDesiredPosition(Vector3 x)
    {

        x = new Vector3((float)Math.Round(x.x, 3),
            x.y,
            (float)Math.Round(x.z, 3));
            
        return x;
    }

    bool MouseOffSafeZone()
    {
        bool holder;

        Vector2 centerCamera = new Vector2(Screen.width / 2, Screen.height / 2);

        holder = Vector2.Distance(Input.mousePosition, centerCamera) > (Screen.height/3);

        return holder;
    }
}
