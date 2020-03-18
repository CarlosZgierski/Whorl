using System;
using UnityEngine;

public class InputModule : MonoBehaviour
{
    public event Action<Vector2> OnAxisInput = delegate { };

    public float HorizontalAxis { get; private set; }
    public float VerticalAxis { get; private set; }
    public Vector2 MousePosition { get; private set; }


    public event Action OnMouseButtonPressed = delegate { };
    public event Action OnMouseButton2Pressed = delegate { };

    public event Action OnKeyDownQ = delegate { };
    public event Action OnKeyDownE = delegate { };
    public event Action OnKeyDownR = delegate { };
    public event Action OnKeyDownF = delegate { };
    public event Action<KeyCode> OnSpellKeyDown = delegate { };


    public void StopInput()
    {
        enabled = false;
        HorizontalAxis = 0;
        VerticalAxis = 0;
    }

    public void ResumeInput()
    {
        enabled = true;
    }

    private void OnDisable()
    {
        VerticalAxis = 0f;
        HorizontalAxis = 0f;
    }


    void Update()
    {
        if (Time.timeScale > 0)
        {
            GetAxisInput();
            GetMouseButtons();
            GetSpellKeys();
            GetMousePosition();
        }
    }
    public void GetMousePosition()
    {
        MousePosition = Input.mousePosition;
    }

    void GetAxisInput()
    {
        OnAxisInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        HorizontalAxis = Input.GetAxis("Horizontal");
        VerticalAxis = Input.GetAxis("Vertical");
    }

    void GetMouseButtons()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space))
        {
            OnMouseButtonPressed();
        }
        if (Input.GetMouseButtonDown(1) || Input.GetKey(KeyCode.LeftShift))
        {
            OnMouseButton2Pressed();
        }
    }

    void GetSpellKeys()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnKeyDownQ();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnKeyDownE();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnKeyDownR();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnKeyDownF();
        }
    }

}
