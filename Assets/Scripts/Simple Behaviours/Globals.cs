using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static GameObject PLAYER_REF;

    public GameObjectVariable playerSciptable;

    private void Start()
    {
        PLAYER_REF = playerSciptable.Value;
    }
}
