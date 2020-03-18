
using UnityEngine;
using UnityEditor;

public class RotationModule : MonoBehaviour {

    public AnimationEvents_Struggler AnimationEvents;

    public Transform TransformToRotate;


    InputModule InputModule;
    StatsModule StatsModule;
    [HideInInspector]
    // speed is the rate at which the object will rotate
    public float rotationSpeed;
    public float rotationSpeedFraction;



    private void Start()
    {
        rotationSpeedFraction = 1;

        InputModule = GetComponent<InputModule>();

        StatsModule = GetComponent<StatsModule>();
        if (StatsModule)
        {
            rotationSpeed = StatsModule.RotationSpeed;
            StatsModule.OnRotationSpeedChange += SetRotationSpeed;
        }
        AnimationEvents.OnLowRotationSpeedEvent += SetRotationSpeedFractionToLow;
        AnimationEvents.OnFullRotationSpeedEvent += SetRotationSpeedFractionToFull;
        AnimationEvents.OnZeroRotationSpeedEvent += SetRotationSpeedFractionToZero;
    }

    private void SetRotationSpeed(float x)
    {
        rotationSpeed = x;
    }

    void SetRotationSpeedFractionToLow()
    {
        rotationSpeedFraction = 0.15f;
    }

    void SetRotationSpeedFractionToFull()
    {
        rotationSpeedFraction = 1f;
    }


    void SetRotationSpeedFractionToZero()
    {
        rotationSpeedFraction = 0f;
    }


    void FixedUpdate()
{
    // Generate a plane that intersects the transform's position with an upwards normal.
    Plane playerPlane = new Plane(Vector3.up, transform.position);

    // Generate a ray from the cursor position
    Ray ray = Camera.main.ScreenPointToRay(InputModule.MousePosition);

    // Determine the point where the cursor ray intersects the plane.
    // This will be the point that the object must look towards to be looking at the mouse.
    // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
    //   then find the point along that ray that meets that distance.  This will be the point
    //   to look at.
    float hitdist = 0.0f;
    // If the ray is parallel to the plane, Raycast will return false.
    if (playerPlane.Raycast(ray, out hitdist))
    {
        // Get the point along the ray that hits the calculated distance.
        Vector3 targetPoint = ray.GetPoint(hitdist);

        // Determine the target rotation.  This is the rotation if the transform looks at the target point.
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

        // Smoothly rotate towards the target point.
        TransformToRotate.transform.rotation = Quaternion.Slerp(TransformToRotate.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * rotationSpeedFraction);
    }
}
}


#region Editor
#if UNITY_EDITOR
[CustomEditor(typeof(RotationModule))]
public class RotationModuleEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as RotationModule;


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (myScript.gameObject.GetComponent<StatsModule>())
        {
            GUILayout.Label("StatsModule detected");
            if (EditorApplication.isPlaying)
            {
                GUILayout.Label("View only");
                GUI.enabled = false;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationSpeed"), true);
                GUI.enabled = true;
            }
            else
            {
                GUILayout.Label("You can view values on play");
            }
        }
        else
        {
            GUILayout.Label("StatsModule not detected");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationSpeed"), true);
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("TransformToRotate"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("AnimationEvents"), true);

        serializedObject.ApplyModifiedProperties();



    }

}

#endif
#endregion
