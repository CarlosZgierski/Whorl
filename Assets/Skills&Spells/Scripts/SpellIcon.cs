using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SpellIcon : MonoBehaviour
{
    public bool hasSpell = false;
    public Spell spell;

    [SerializeField]
    Image IconImage;

    [SerializeField]
    Image CooldownImage;
    [SerializeField]
    Text CooldownText;

    private Color ActiveColor = new Color(1, 1, 1, 1);
    private Color CooldownColor = new Color(1, 1, 1, 0.85f);

    public void SetIcon(Spell x)
    {
        hasSpell = true;
        spell = x;
        gameObject.SetActive(true);
        IconImage.sprite = x.IconSprite;
        IconImage.enabled = true;
    }

    public void Disable()
    {
        hasSpell = false;
        spell = null;
        gameObject.SetActive(false);
    }

    public void SubscribeToCooldownEventOnSpellWrapper(SpellCaster_Struggler.StrugglerSpellWrapper wrapper)
    {
        wrapper.OnSpellCooldownChange += CooldownRefresh;

    }

    void CooldownRefresh(SpellCaster_Struggler.StrugglerSpellWrapper wrapper)
    {
        if(wrapper.isOnCooldown)
        {
            IconImage.color = CooldownColor;
            CooldownImage.enabled = true;
            CooldownText.enabled = true;

            CooldownText.text = string.Format("{0:0.0}", wrapper.cooldownTimer);
            //CooldownText.text = wrapper.cooldownTimer.ToString().Substring(0,3);
            
        }
        else
        {
            IconImage.color = ActiveColor;
            CooldownImage.enabled = false;
            CooldownText.enabled = false;
        }
    }
}
