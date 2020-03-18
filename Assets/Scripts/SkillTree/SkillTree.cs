using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public enum Mode
    {
        Build,
        Sacrifice,
        Cheat
    }

    public Mode CurrentMode;

    public SkillTreeUI SkillTreeUI;

    [SerializeField]
    ListOfSkills ownedSkills;
    public ListOfSkills OwnedSkills { get { return ownedSkills; } }

    public SkillDisplay[] OwnedSkillDisplays;
    Skill[] SkillsToBuy;
    Skill SkillToSacrifice;

    public SkillDisplay[] Displays;

    int numberOfSkillsToBuy;
    int numberOfSkillsToSacrifice;

    public GameEvent OnSkillsetChangeEvent;

    [Space]
    public AudioClip SacrificeStartClip;
    public AudioClip SacrificeEndClip;
    public AudioClip BuySkillClip;
    public AudioClip UnbuySkillClip;
    public AudioSource AudioSource;
    public GameObject DisposableAudioSource;

    private void Start()
    {
         ownedSkills.CleanList();
        SetModeTo(Mode.Build);
        RefreshUI();
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        RefreshUI();

    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        RefreshUI();
    }

    private void RefreshDisplayColor()
    {
        foreach (SkillDisplay x in Displays)
        {

            if (OwnedSkills.list.Contains(x.Skill))
            {
                x.SetColorToGrey(true);
            }
            else
            {
                x.SetColorToGrey(false);
            }


        }
    }

    void RefreshUI()
    {
        //SkillTreeUI.RefreshUI(CurrentMode, numberOfSkillsToBuy, numberOfSkillsToSacrifice);
    }

    void RefreshOwnedSkillsDisplay()
    {
        foreach (SkillDisplay x in OwnedSkillDisplays)
        {
            x.ResetDisplay();
        }
        for (int i = 0; i < OwnedSkills.list.Count; i++)
        {
            OwnedSkillDisplays[i].SetDisplayToSkill(OwnedSkills.list[i]);
        }
    }

    void BuySkill(Skill x)
    {
        OwnedSkills.list.Add(x);
        numberOfSkillsToBuy--;

    }

    void UnbuySkill(Skill x)
    {
        OwnedSkills.list.Remove(x);
        numberOfSkillsToBuy++;
    }

    void UnsacrificeSkill(Skill x)
    {
        numberOfSkillsToSacrifice++;
        GetSkillDisplayBySkill(x).SetSacrificeImageTo(false);


    }

    SkillDisplay GetSkillDisplayBySkill(Skill x)
    {
        foreach(SkillDisplay p in OwnedSkillDisplays)
        {
            if(p.Skill == x)
            {
                return p;
            }
        }
        return null;
    }

    void SacrificeSkill(Skill x)
    {
        if(SkillToSacrifice != null)
        {
            UnsacrificeSkill(SkillToSacrifice);
        }
        numberOfSkillsToSacrifice--;

        SkillToSacrifice = x;

    }



    public void SkillDisplayClicked(SkillDisplay display, Skill skillDisplayed)
    {
        if(OwnedSkills.list.Contains(skillDisplayed))
        {
            if(CurrentMode != Mode.Sacrifice)
            {
                UnbuySkill(skillDisplayed);
                RefreshOwnedSkillsDisplay();
                AudioSource.PlayOneShot(UnbuySkillClip);
            }
            else
            {
                SacrificeSkill(skillDisplayed);
                display.SetSacrificeImageTo(true);
                
            }
        }
        else
        {
            if (CurrentMode != Mode.Sacrifice)
            {
                if (numberOfSkillsToBuy > 0)
                {
                    AudioSource.PlayOneShot(BuySkillClip);
                    BuySkill(skillDisplayed);
                    RefreshOwnedSkillsDisplay();
                }
            }
        }
        

        RefreshUI();
        RefreshDisplayColor();
        SkillTreeUI.RefreshUI(CurrentMode, numberOfSkillsToBuy, numberOfSkillsToSacrifice);

    }

    public void SetSkillDisplays(bool x)
    {
        foreach(SkillDisplay o in Displays)
        {
            o.gameObject.SetActive(x);
        }
    }

    public void SetModeTo(Mode newMode)
    {
        if(newMode == Mode.Sacrifice)
        {
            SkillToSacrifice = null;
            numberOfSkillsToBuy = 0;
            numberOfSkillsToSacrifice = 1;
            SetSkillDisplays(false);
            gameObject.SetActive(true);

        }
        else if(newMode == Mode.Build)
        {
            
            numberOfSkillsToBuy = 3;
            numberOfSkillsToSacrifice = 0;
            SetSkillDisplays(true);
            gameObject.SetActive(true);
        }
        else
        {
            numberOfSkillsToBuy = 100;
            numberOfSkillsToSacrifice = 0;
            SetSkillDisplays(true);
            gameObject.SetActive(true);

        }
        CurrentMode = newMode;
        SkillTreeUI.RefreshUI(CurrentMode, numberOfSkillsToBuy, numberOfSkillsToSacrifice);
        RefreshUI();
    }

    private void CreateFuckingAudioSourceJustToPlaySacrificeEndClip()
    {
        GameObject x = new GameObject();
        x.AddComponent<AudioSource>();
        AudioSource y = x.GetComponent<AudioSource>();
        y.outputAudioMixerGroup = AudioSource.outputAudioMixerGroup;
        y.PlayOneShot(SacrificeEndClip);
        Destroy(x, SacrificeEndClip.length);
    }

    public void ContinueButtonPressed()
    {
        if (CanContinue())
        {
            if (CurrentMode == Mode.Sacrifice)
            {
                OwnedSkills.list.Remove(SkillToSacrifice);
                CreateFuckingAudioSourceJustToPlaySacrificeEndClip();


                //PlaySacrificeSound();
            }

            OnSkillsetChangeEvent.Raise();
            gameObject.SetActive(false);

            /*foreach (SkillDisplay p in SkillDisplays)
            {

                p.gameObject.SetActive(true);

            }*/

        }
    }

    bool CanContinue()
    {
        return ((numberOfSkillsToBuy == 0 && CurrentMode == Mode.Build) || (numberOfSkillsToSacrifice == 0 && CurrentMode == Mode.Sacrifice) || CurrentMode == Mode.Cheat);
    }

    public void TriggerSkillSacrifice()
    {

        SetModeTo(Mode.Sacrifice);
        AudioSource.PlayOneShot(SacrificeStartClip);

    }

    public void TriggerSkillCheat()
    {
        SetModeTo(Mode.Cheat);
    }





}
