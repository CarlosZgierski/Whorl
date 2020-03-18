using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillTreeOld : MonoBehaviour
{
    /*
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

    SkillDisplay[] SkillDisplays;
    public GameEvent OnSkillsetChangeEvent;
    public int skillsToBuy = 3;

    [Space]
    [SerializeField]
    AudioClip SacrificeContinueSound;
    [SerializeField]
    AudioClip BuySkillSound;
    [SerializeField]
    AudioClip LoseSkillSound;

    AudioSource AudioSource;

    #region MonoBehaviour Methods

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        ownedSkills.CleanList();
        GetSkillDisplays();
        SetDisplays();
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

    #endregion

    #region Public Methods

    public void SetDisplays()
    {
        for(int i = 0; i < SkillDisplays.Length; i++)
        {
            SkillDisplays[i].SetDisplay();
        }
    }

    public void ContinueButtonPressed()
    {
        if (CanContinue())
        {
            if(CurrentMode == Mode.Sacrifice)
            {
                PlaySacrificeSound();
            }
      
            OnSkillsetChangeEvent.Raise();
            gameObject.SetActive(false);
            
            foreach (SkillDisplay p in SkillDisplays)
            {
                
                    p.gameObject.SetActive(true);
               
            }

        }
    }

    public void ClickOnSkill(SkillDisplay display, Skill skill)
    {
        if (display.owned)
        {
            if (CurrentMode == Mode.Sacrifice)
            {
                if (skillsToBuy < 0)
                {
                    AudioSource.PlayOneShot(LoseSkillSound);
                    RemoveSkill(skill);
                    display.owned = false ;

                }
            }
            else
            {
                AudioSource.PlayOneShot(LoseSkillSound);
                RemoveSkill(skill);
                display.owned = false;
            }

        }
        else
        {
            if (CurrentMode != Mode.Sacrifice)
            {
                AudioSource.PlayOneShot(BuySkillSound);
                display.owned = true;
                BuySkill(skill);
            }
        }

    }

    public void BuySkill(Skill x)
    {
        ownedSkills.AddSkill(x);
        skillsToBuy--;
        RefreshUI();

    }

    public void TriggerSkillSacrifice()
    {
        foreach (SkillDisplay p in SkillDisplays)
        {
            if (p.owned == false)
            {
                p.gameObject.SetActive(false);
            }
        }
        gameObject.SetActive(true);
        CurrentMode = Mode.Sacrifice;
        skillsToBuy = -1;
        RefreshUI();
        PlaySacrificeSound();

    }

    public void TriggerSkillCheat()
    {
        gameObject.SetActive(true);
        CurrentMode = Mode.Cheat;
        skillsToBuy = 100;
        RefreshUI();
    }

    public void RemoveSkill(Skill x)
    {
        ownedSkills.RemoveSkill(x);
        skillsToBuy++;
        RefreshUI();

    }
#endregion

    #region Private Methods

    void RefreshUI()
    {
        SkillTreeUI.RefreshUI(CurrentMode, skillsToBuy);

    }

    bool CanContinue()
    {
        return (skillsToBuy == 0 || CurrentMode == Mode.Cheat);
    }

    void PlaySacrificeSound()
    {
        AudioSource.Play();
    }

    void PlayPostSacrificeSound()
    {
        GameObject DisposableAudioSource = new GameObject("SkillSacrificeAudioSource", typeof(AudioSource));
        DisposableAudioSource.GetComponent<AudioSource>().PlayOneShot(SacrificeContinueSound);
        Destroy(DisposableAudioSource, SacrificeContinueSound.length);
    }

    void GetSkillDisplays()
    {
        SkillDisplays = GetComponentsInChildren<SkillDisplay>();
    }
    #endregion


    */


}
