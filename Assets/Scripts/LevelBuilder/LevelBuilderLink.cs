using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderLink : MonoBehaviour
{
    public GameObjectVariable LevelBuilderReference;
    
    public void NewLevelSegment()
    {
        LevelBuilderReference.Value.GetComponent<LevelBuilder>().InstantiateNextSegment();
    }
}
