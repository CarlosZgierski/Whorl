
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillInSkillTree : MonoBehaviour
{
    Vector3 originalLocalPosition;

    private bool isOnSkillField;
    private SkillTreeSkillSlot skillSlot = null;
    public Skill skill;
    private Transform currentSkillFieldTransform;

    [SerializeField]
    CircleCollider2D SkillCollider2D;

    [SerializeField]
    Image skillImage;

    [SerializeField]
    GameObject description;

    [SerializeField]
    TextMeshProUGUI nameText;
    [SerializeField]
    TextMeshProUGUI descriptionText;
    [SerializeField]
    Image hudImage;
    
    

    void Start()
    {
        originalLocalPosition = transform.localPosition;
        
    }

   public void Initialize()
    {
        skillImage.sprite = skill.SpriteInSkillTree;
        nameText.text = skill.Name;
        descriptionText.text = skill.Description;
        hudImage.sprite = skill.SpriteInHUD;
    }


    public void MouseEnter()
    {
        GetComponent<RectTransform>().SetAsLastSibling();
        description.SetActive(true);
    }

    public void MouseExit()
    {
        description.SetActive(false);
    }

    public void BeginDrag()
    {
        description.SetActive(false);
    }

    public void DragRelease()
    {
        bool collisionSustained = false;
        foreach (Collider2D x in Physics2D.OverlapCircleAll(transform.position, SkillCollider2D.radius, ~0))
        {
            if(x.CompareTag("SkillField"))
            {
                if(skillSlot != null && skillSlot.SelectedSkill == this)
                {
                    skillSlot.RemoveSkill();
                }
                collisionSustained = true;
                skillSlot = x.GetComponent<SkillTreeSkillSlot>();
                isOnSkillField = true;
                currentSkillFieldTransform = x.transform;
            }
        }
        if(collisionSustained == false)
        {
            if (skillSlot)
            {
                if (skillSlot.SelectedSkill == this)
                {
                    skillSlot.RemoveSkill();
                    
                    
                }
            }
            currentSkillFieldTransform = null;
            skillSlot = null;
            isOnSkillField = false;
        }


        if(isOnSkillField && skillSlot.SelectedSkill == null)
        {
            transform.localPosition = currentSkillFieldTransform.transform.localPosition;
            skillSlot.SelectSkill(this);
        }
        else
        {
            if(skillSlot != null && skillSlot.SelectedSkill == this)
            {
                skillSlot.RemoveSkill();
            }
            isOnSkillField = false;
            skillSlot = null;
            currentSkillFieldTransform = null;
            transform.localPosition = originalLocalPosition;
            if(skillSlot)
            {
                
            }
        }
        
    }

    /*

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SkillField"))
        {
           
            skillSlot = collision.GetComponent<SkillTreeSkillSlot>();

            isOnSkillField = true;
            currentSkillFieldTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SkillField"))
        { 
            if(skillSlot)
            {
                if(skillSlot.SelectedSkill == this)
                {
                    skillSlot.RemoveSkill();
                    skillSlot = null;
                    currentSkillFieldTransform = null;
                }
            }
            isOnSkillField = false;
            
            
        }
    }
    */

}
