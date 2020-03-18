using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class must be in a Gameobject that is a children from the main Player GameObject and the Z axis must be looking foward always
public class CameraTargetLerper : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject playerRef;

    //Both in the Z axis
    [SerializeField] private float minLerpValue = 0.1f; 
    [SerializeField] private float maxLerpValue = 0.5f;

    private float windowSize_X;
    private float windowSize_Y;

    private float distanceToBorderX_Normalized;
    private float distanceToBorderY_Normalized;

    private float biggestNormalized;
    private float zPosition;

    void Start()
    {
        if(playerRef == null)
        {
            playerRef = GetComponentInParent<GameObject>();
        }

        windowSize_X = playerCamera.pixelWidth;
        windowSize_Y = playerCamera.pixelHeight;
    }

    void LateUpdate()
    {
        distanceToBorderX_Normalized = (Input.mousePosition.x - (windowSize_X / 2)) / (windowSize_X / 2);
        distanceToBorderY_Normalized = (Input.mousePosition.y - (windowSize_Y / 2)) / (windowSize_Y / 2);

        if (distanceToBorderX_Normalized < 0) distanceToBorderX_Normalized = distanceToBorderX_Normalized * -1;
        if (distanceToBorderY_Normalized < 0) distanceToBorderY_Normalized = distanceToBorderY_Normalized * -1;

        biggestNormalized = distanceToBorderX_Normalized;
        if (distanceToBorderY_Normalized > biggestNormalized) biggestNormalized = distanceToBorderY_Normalized;

        zPosition = Mathf.Lerp(minLerpValue, maxLerpValue, biggestNormalized);

        /*
        if (playerRef.transform.eulerAngles.y >293 || playerRef.transform.eulerAngles.y < 65)
        {
            zPosition += 1;
        }*/

        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, zPosition);


#if UNITY_EDITOR
        //Debug.Log("Player Rotation(Euler angle)" + playerRef.transform.eulerAngles.y);
       // Debug.Log("Zposition" + zPosition);
        //Debug.Log("bagulho normalizado" + distanceToBorderX_Normalized);
        //Debug.Log("Posicão X do mouse:" + (Input.mousePosition.x - (playerCamera.pixelWidth / 2 )));
        //Debug.Log("Posicão Y do mouse:" + (Input.mousePosition.y - (playerCamera.pixelHeight / 2 )));
#endif
    }
}
