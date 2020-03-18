using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentActivationTrigger : MonoBehaviour
{
    [SerializeField]
    bool isPastSegmentTrigger;
    [SerializeField]
    bool isBothTrigger = false;

    [SerializeField]
    GameObjectVariable levelBuilderReference;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthController x) && x.entityType == EntityTypes.Player)
        {
            Trigger();
        }
    }

    private void Trigger()
    {
        LevelBuilder levelBuilder = levelBuilderReference.Value.GetComponent<LevelBuilder>();
        if (isBothTrigger)
        {
            levelBuilder.SetActiveOfNextSegmentTo(true, transform.root.gameObject);
            levelBuilder.SetActiveOfPastSegmentTo(true, transform.root.gameObject);
        }
        else
        {
            if (isPastSegmentTrigger)
            {
                levelBuilder.SetActiveOfNextSegmentTo(false, transform.root.gameObject);
                levelBuilder.SetActiveOfPastSegmentTo(true, transform.root.gameObject);
            }
            else
            {
                levelBuilder.SetActiveOfNextSegmentTo(true, transform.root.gameObject);
                levelBuilder.SetActiveOfPastSegmentTo(false, transform.root.gameObject);
            }
        }
       
    }
}
