using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFade : MonoBehaviour
{
    private GameObject playerRef;
    private Image[] healthImages;

    private float fullOpaque_dist = 3f;
    private float fullTransparent_dist = 12f;

    void Start()
    {
        playerRef = Globals.PLAYER_REF;
        healthImages = this.gameObject.GetComponentsInChildren<Image>();
    }

    void Update()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            var color = healthImages[i].color;
            color.a = CheckPlayerDistance();

            healthImages[i].color = color;
        }
    }

    float CheckPlayerDistance ()
    {
        float dist;
        dist = Vector3.Distance(this.transform.position, playerRef.transform.position);

        if (dist <= fullOpaque_dist)
        {
            return 1f;
        }
        else if(dist > fullTransparent_dist)
        {
            return 0f;
        }
        else
        {
            float normDist;

            normDist = (((dist / fullTransparent_dist) -1 )*-1) + 0.2f;

            return normDist;
        }
    }
}
