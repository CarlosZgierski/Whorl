﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTestUpdatePrinter : MonoBehaviour
{
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Struggler").transform;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
