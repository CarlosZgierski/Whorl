using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Cheats))]
public class CheatSystem : MonoBehaviour {

    
    public UseCheat[] UseCheats;
    public ModeCheat[] ModeCheats;


    // Use this for initialization
    void Start () {
#if UNITY_EDITOR
        Debug.Log(" ");
        Debug.LogWarning("Cheats are activated!");
        PrintAllCheats();
#endif


    }

    private void Update()
    {
        CheckCheatKeys();
    }

    /*
    void CheckCheatKeys()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
        {
           
        foreach (UseCheat c in UseCheats)
        {
            if (Input.GetKeyDown(c.CheatKey))
            {
                    
                    PlaySound();
#if UNITY_EDITOR
                    Debug.Log("Cheat " + "'" + c.CheatName + "' used!");
#endif
                    c.CheatEvent.Invoke();
            }
        }
            foreach (ModeCheat c in ModeCheats)
            {
                if (Input.GetKeyDown(c.CheatKey))
                {
                    Debug.Log("Done");
                    PlaySound();
                    c.isOn = !c.isOn;
                    bool b = true;
                    if (c.isOn != true && b)
                    {
                        b = false;
#if UNITY_EDITOR
                        Debug.Log("Cheat " + "'" + c.CheatName + "' is off!");
#endif
                        c.DisableCheatEvent.Invoke();

                    }
                    else if (b)
                    {
                        
                        b = false;
#if UNITY_EDITOR
                        Debug.Log("Cheat " + "'" + c.CheatName + "' is on!");
#endif
                        c.ActiveCheatEvent.Invoke();
                    }



                }
            }
        }


       
    }
    */
    
    
    void CheckCheatKeys()
    {
       
            foreach (UseCheat c in UseCheats)
            {
                if (Input.GetKeyDown(c.CheatKey))
                {

                    PlaySound();
#if UNITY_EDITOR
                    Debug.Log("Cheat " + "'" + c.CheatName + "' used!");
#endif
                    c.CheatEvent.Invoke();
                }
            }
            foreach (ModeCheat c in ModeCheats)
            {
                if (Input.GetKeyDown(c.CheatKey))
                {
                    PlaySound();
                    c.isOn = !c.isOn;
                    bool b = true;
                    if (c.isOn != true && b)
                    {
                        b = false;
#if UNITY_EDITOR
                        Debug.Log("Cheat " + "'" + c.CheatName + "' is off!");
#endif
                        c.DisableCheatEvent.Invoke();

                    }
                    else if (b)
                    {

                        b = false;
#if UNITY_EDITOR
                        Debug.Log("Cheat " + "'" + c.CheatName + "' is on!");
#endif
                        c.ActiveCheatEvent.Invoke();
                    }



                }
            }
        



    }
   

    private void PlaySound()
    {
        if (GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().Play();
        }
    }


    public void PrintAllCheats()
    {
#if UNITY_EDITOR
        Debug.Log("ACTIVE CHEATS:");
        foreach (UseCheat c in UseCheats)
        {
            Debug.Log("==>" + c.CheatName + " (use): Alt + " + c.CheatKey.ToString());
        }
        foreach (ModeCheat c in ModeCheats)
        {
            Debug.Log("==>" + c.CheatName + " (mode): Alt + " + c.CheatKey.ToString());
        }
        Debug.Log(" ");
#endif
    }

    public void PrintFuck()
    {
        print("fuck");
    }



}

[System.Serializable]
public class UseCheat
{
    public string CheatName;
    public KeyCode CheatKey;

    public UnityEvent CheatEvent;
    
}


[System.Serializable]
public class ModeCheat
{
    public string CheatName;
    public KeyCode CheatKey;
    public bool isOn = false;

    public UnityEvent ActiveCheatEvent;
    public UnityEvent DisableCheatEvent;

}


