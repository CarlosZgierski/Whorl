using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SkillSacrificeDirector : MonoBehaviour
{
    [SerializeField]
    SkillInSkillTree[] SkillImages;
    [SerializeField]
    ListOfSkills ownedSkills;

    [SerializeField]
    SkillSacrificePopup skillSacrificePopup;
    
    private SkillInSkillTree skillToSacrifice;

    [SerializeField]
    GameEvent OnSkillSetChangeGameEvent;

    [SerializeField]
    GameObjectVariable skillTreeReference;

    [SerializeField]
    AudioSource sacrificeRequiredAudioSource;

    [SerializeField]
    AudioClip sacrificeClipForDisposableAudioSource;
    [SerializeField]
    AudioMixerGroup mixerForDisposableAudioSource;



    private void Start()
    {
        gameObject.SetActive(false);
    }


    void SetSkillImages()
    {
        foreach(SkillInSkillTree x in SkillImages)
        {
            x.skill = null;
        }
        for(int i = 0; i < ownedSkills.list.Count; i++)
        {
            SkillImages[i].skill = ownedSkills.list[i];
        }
       
        foreach (SkillInSkillTree x in SkillImages)
        {
            if(x.skill == null)
            {
                x.gameObject.SetActive(false);
            }
            else
            {
                x.gameObject.SetActive(true);
                x.Initialize();
            }
        }
        
    

    }

    public void SacrificeSkill()
    {
        ownedSkills.RemoveSkill(skillToSacrifice.skill);
        OnSkillSetChangeGameEvent.Raise();
        gameObject.SetActive(false);
    }

    public void ClickedOnSkill(SkillInSkillTree x)
    {
        skillSacrificePopup.gameObject.SetActive(true);
        skillSacrificePopup.SetToSkill(x);
        skillToSacrifice = x;
    }


    private void OnEnable()
    {
        if (!FindObjectOfType<SkillTreeDirector>())
        {
            Time.timeScale = 0f;
            FindObjectOfType<InputModule>().enabled = false;
            FindObjectOfType<FollowTargetPosition>().enabled = false;
            SetSkillImages();
            sacrificeRequiredAudioSource.Play();
        }
        
    }

    private void OnDisable()
    {
        if (!FindObjectOfType<SkillTreeDirector>())
        {
            Time.timeScale = 1f;
            FindObjectOfType<InputModule>().enabled = true;
            FindObjectOfType<FollowTargetPosition>().enabled = true;
            skillSacrificePopup.gameObject.SetActive(false);
            CreateDisposableAudioSourceAndPlaySacrificeSound();
        }
    }

    private void CreateDisposableAudioSourceAndPlaySacrificeSound()
    {
        AudioSource x = new GameObject("sacrificeSound", typeof(AudioSource)).GetComponent<AudioSource>();
        Destroy(x.gameObject, sacrificeClipForDisposableAudioSource.length);
        x.clip = sacrificeClipForDisposableAudioSource;
        x.outputAudioMixerGroup = mixerForDisposableAudioSource;
        x.Play();
    }

  
}
