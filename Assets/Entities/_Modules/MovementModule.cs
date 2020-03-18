using System;
using UnityEngine;
using UnityEditor;
using System.Collections;

public class MovementModule : MonoBehaviour
{


    [SerializeField]
    float speed;
    float speedFraction = 1f;
    float currentDirectionMultiplier;
    InputModule InputModule;
    StatsModule StatsModule;


    [SerializeField]
    float frontDirectionSpeedMultiplier = 1f;
    [SerializeField]
    float sideDirectionSpeedMultiplier = 0.75f;
    [SerializeField]
    float backDirectionSpeedMultiplier = 0.5f;

    Rigidbody Rigidbody;
    bool dodge = false;

    [SerializeField]
    private float dodgeCooldown = 0.7f;
    private float nextTimeToDodge = 0;

    [SerializeField]
    private float dodgeSpeed;

    public event Action OnStill = delegate {};
    public event Action OnMove = delegate { };
    public event Action OnDodge = delegate { };

    private WalkingDirections currentDirection;
    public WalkingDirections CurrentDirection
    {
        get
        {
            return currentDirection;
        }
    }

    [SerializeField]
    AnimationEvents_Struggler AnimationEvents;


    void Start()
    {
        StatsModule = GetComponent<StatsModule>();
        if(StatsModule)
        {
            speed = StatsModule.Speed;
            StatsModule.OnSpeedChange += SetSpeedTo;
            dodgeSpeed = StatsModule.DodgeSpeed;
            StatsModule.OnDodgeSpeedChange += SetDodgeSpeedTo;
        }

        Rigidbody = GetComponent<Rigidbody>();

        InputModule = GetComponent<InputModule>();
        InputModule.OnMouseButton2Pressed += DodgeButtonPressed;

        AnimationEvents.OnAddDodgeForceEvent += AddDodgeForce;
        AnimationEvents.OnLowSpeedEvent += SetSpeedFractionToLow;
        AnimationEvents.OnFullSpeedEvent += SetSpeedFractionToFull;
        AnimationEvents.OnZeroSpeedEvent += SetSpeedFractionToZero;
    }

  

    void DodgeButtonPressed()
    {
        if ((InputModule.HorizontalAxis != 0 || InputModule.VerticalAxis != 0) && Time.time > nextTimeToDodge)
        {
            nextTimeToDodge = Time.time + dodgeCooldown;
            OnDodge();
            dodge = true;//============================
            StartCoroutine(TurnDodgeOff());
        }
    }
  
    void FixedUpdate()
    {
        
        if (InputModule.HorizontalAxis == 0 && InputModule.VerticalAxis == 0)
        {
            Still();
        }
        else
        {
            currentDirection = GetDirection();
            currentDirectionMultiplier = GetDirectionMultiplier(currentDirection);


            Vector3 InputVector = new Vector3(InputModule.HorizontalAxis, 0, InputModule.VerticalAxis);
            if(InputVector.magnitude > 1)
            {
                InputVector = InputVector.normalized;
            }
            Vector3 amountToMove = InputVector * speed * speedFraction * currentDirectionMultiplier;
            if (!dodge)
            {
                Move(amountToMove);
            }
        }
    }

    void Move(Vector3 x)
    {
        OnMove();
        Rigidbody.AddForce(x / 10f, ForceMode.Impulse);
        //Debug.Log(Rigidbody.velocity + "    " + Rigidbody.velocity.magnitude);
    }


    void Still()
    {
        OnStill();
    }

    WalkingDirections GetDirection()
    {
        float distanceFromMovementDirectionAndRotationForward = Vector3.Distance(Rigidbody.velocity.normalized, transform.forward); //vai de 0 até 2

        if (distanceFromMovementDirectionAndRotationForward < 0.7f)
        {
            return WalkingDirections.Front;
        }
        else if (distanceFromMovementDirectionAndRotationForward < 1.7f)
        {
            return (IsSideIsLeft() ? WalkingDirections.Left : WalkingDirections.Right);
        }
        return WalkingDirections.Back;



        bool IsSideIsLeft()
        {
            return Vector3.Distance(Rigidbody.velocity.normalized, transform.right) > Vector3.Distance(Rigidbody.velocity.normalized, -transform.right);
        }
    }

    float GetDirectionMultiplier(WalkingDirections x)
    {
       switch (x) {

            case WalkingDirections.Front:
                return frontDirectionSpeedMultiplier;

            case WalkingDirections.Left:
            case WalkingDirections.Right:
                return sideDirectionSpeedMultiplier;

            case WalkingDirections.Back:
                return backDirectionSpeedMultiplier;

            default:
                return 30000;

        }
    }

    private IEnumerator TurnDodgeOff()//============================
    {
        yield return new WaitForSeconds(0.55f);//============================
        dodge = false;//============================
        Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, 7);
       

    }

    void AddDodgeForce()
    {
        Rigidbody.velocity = Vector3.zero;
       
         Rigidbody.AddForce(new Vector3(InputModule.HorizontalAxis, 0, InputModule.VerticalAxis).normalized * dodgeSpeed, ForceMode.Impulse);//============================
    }

    public enum WalkingDirections
    {
        Front,
        Back,
        Left, 
        Right
    }


 #region Events
    void SetSpeedFractionToLow()
    {
        speedFraction = 0.15f;
    }

    void SetSpeedFractionToFull()
    {
        speedFraction = 1f;
    }

    void SetSpeedFractionToZero()
    {
        speedFraction = 0f;
    }

    void SetDodgeSpeedTo(float x)
    {
        dodgeSpeed = x;
    }

    void SetSpeedTo(float x)
    {
        speed = x;
    }

    #endregion
}


#region Editor
#if UNITY_EDITOR
[CustomEditor(typeof(MovementModule))]
public class MovementModuleEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as MovementModule;


        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (myScript.gameObject.GetComponent<StatsModule>())
        {
            GUILayout.Label("StatsModule detected");
            if (EditorApplication.isPlaying)
            {
                GUILayout.Label("View only");
                GUI.enabled = false;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"), true);
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"), true);
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("frontDirectionSpeedMultiplier"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sideDirectionSpeedMultiplier"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("backDirectionSpeedMultiplier"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("dodgeCooldown"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("AnimationEvents"), true);

        serializedObject.ApplyModifiedProperties();



    }

}

#endif
#endregion
