using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillDisplay : MonoBehaviour
{
    public bool isOwnedSkillDisplay = false;
    public SkillTree SkillTree;
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
    [SerializeField]
    private Image sacrificeImage;






    void Start()
    {
        SetDescriptionEnabledTo(false);
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

    public void ResetDisplay()
    {
        Skill = null;
        title.SetText(" ");
        description.text = "";
        image.sprite = null;

    }

    public void Clicked()
    {
        SkillTree.SkillDisplayClicked(this, Skill);


    }

    public void SetSacrificeImageTo(bool x)
    {
        sacrificeImage.enabled = x;
    }

    public void SetDescriptionEnabledTo(bool x)
    {
        description.enabled = x;
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
