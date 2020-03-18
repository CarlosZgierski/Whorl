using UnityEngine.UI;
using UnityEngine;

public class SkillTreeDirector : MonoBehaviour
{
    [SerializeField]
    SkillTreeSkillSlot[] skillSlots;
    [SerializeField]
    SkillInSkillTree[] skills;

    [SerializeField]
    GameObject startButton;

    [SerializeField]
    ListOfSkills OwnedSkills;
    [SerializeField]
    GameEvent OnSkillsetChangeGameEvent;

    [SerializeField]
    AudioSource gameStartAudioSource;

   

    private void OnEnable()
    {
        Time.timeScale = 0;
        
        foreach(SkillInSkillTree x in skills)
        {
            x.Initialize();
        }
   
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        foreach (SkillTreeSkillSlot x in skillSlots)
        {
            x.OnSkillSelect += SkillSelectEventHandler;
            x.OnSkillRemove += SkillRemoveEventHandler;
        }
    }

   

    void SkillSelectEventHandler()
    {
        bool allSkillsSelected = true;
        foreach (SkillTreeSkillSlot x in skillSlots)
        {
            if (x.SelectedSkill == null)
            {
                allSkillsSelected = false;
            }

        }
        if (allSkillsSelected)
        {
            startButton.SetActive(true);
        }
    }

    void SkillRemoveEventHandler()
    {
        startButton.SetActive(false);
    }

    public void StartGame()
    {
        gameStartAudioSource.Play();
        gameStartAudioSource.gameObject.transform.parent = null;

        OwnedSkills.list.Clear();
        foreach (SkillTreeSkillSlot x in skillSlots)
        {
            OwnedSkills.AddSkill(x.SelectedSkill.skill);
        }
        OnSkillsetChangeGameEvent.Raise();
        gameObject.SetActive(false);

    }
}
