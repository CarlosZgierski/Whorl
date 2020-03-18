using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillDisplayOld : MonoBehaviour
{
    public bool isOwnedSkillDisplay = false;
    SkillTree SkillTree;
    [SerializeField]
    public Skill Skill;
    public bool owned = false;
    public bool locked = false;

    [Space]
    [Space]
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private Image image;






    void Start()
    {
        SkillTree = FindObjectOfType<SkillTree>().GetComponent<SkillTree>();
        if (Skill)
        {
            SetDisplay();
        }
    }

    public void SetDisplayToSkill(Skill x)
    {
        Skill = x;
        SetDisplay();
    }

    public void SetDisplay()
    {
        title.SetText(Skill.Name);
        description.text = Skill.Description;
        image.sprite = Skill.SpriteInSkillTree;

    }

   

    public void Clicked()
    {
        SkillTree.SkillDisplayClicked(null, Skill);//null foi mudado


        RefreshUI();

    }

    public void SetDescriptionEnabledTo(bool x)
    {
        description.enabled = x;
    }

    public void RefreshUI()
    {
        if (owned)
        {
            SetColorToGrey(true);

        }
        else
        {
            SetColorToGrey(false);
        }
    }

    public void SetColorToGrey(bool x)
    {
        if (x)
        {
            title.color = Color.grey;
            image.color = Color.grey;
            description.color = Color.grey;
        }
        else
        {
            title.color = Color.white;
            image.color = Color.white;
            description.color = Color.white;
        }
    }


}
