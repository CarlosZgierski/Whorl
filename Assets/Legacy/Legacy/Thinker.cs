using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thinker : MonoBehaviour
{
    [SerializeField]
    Brain BrainTemplate;
    private Brain Brain;

    private void Start()
    {
        Brain = Instantiate(BrainTemplate);
        Brain.Initialize(this);
    }

    void Update()
    {
        Brain.Think();
    }
}
