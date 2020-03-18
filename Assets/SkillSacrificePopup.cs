
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSacrificePopup : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI skillName;
    [SerializeField]
    Image HUDImage;


    public void SetToSkill(SkillInSkillTree x)
    {
        skillName.text = x.skill.Name;
        HUDImage.sprite = x.skill.SpriteInHUD;
    }
}
