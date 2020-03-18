using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationModule_Enemy : MonoBehaviour
{
    NavMeshBrain NavMeshBrain;
    [SerializeField]
    Transform target;
    [SerializeField]
    float rotationSpeed = 10;

    private void Start()
    {
        NavMeshBrain = GetComponent<NavMeshBrain>();
        if (NavMeshBrain)
        {
            NavMeshBrain.OnAttack += LoseTarget;
            NavMeshBrain.OnTargetOnAttackRange += SetTarget;
            NavMeshBrain.OnStartChasingTarget += LoseTarget;

        }
    }
    void Update()
    {
        if (target)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void EnableRotationToFaceTarget(Transform target)
    {
        enabled = true;
        this.target = target;
    }

    public void DisableRotation()
    {
        enabled = false;
        target = null;
    }


    void SetTarget(GameObject target)
    {
        this.target = target.transform;
    }

    void LoseTarget()
    {
        target = null;
    }

    void LoseTarget(GameObject x)
    {
        target = null;
    }
}
