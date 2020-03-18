using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillTreeUI : MonoBehaviour
{
    [SerializeField]
    Button ContinueButton;
    [Space]

    [SerializeField]
    TMP_Text SkillsToBuyText;
    [SerializeField]
    TMP_Text SkillTreeText;
    [Space]
    [SerializeField]
    string BuildText;
    [SerializeField]
    string SacrificeText;
    [SerializeField]
    string CheatText;


    public void RefreshUI(SkillTree.Mode CurrentMode, int skillsToBuy, int skillsToSacrifice)
    {
        SetSkillTreeText();
        SetSkillsToBuyText();
        ContinueButton.interactable = IsContinueButtonInteractable();


        void SetSkillsToBuyText()
        {
            if (CurrentMode == SkillTree.Mode.Build)
            {
                if (skillsToBuy > 0)
                {
                    SkillsToBuyText.text = "Number of skills to buy: <b>" + skillsToBuy.ToString() + "</b>";
                }

                else if (skillsToBuy == 0)
                {
                    SkillsToBuyText.text = "You can continue";
                }
            }
            else if (CurrentMode == SkillTree.Mode.Sacrifice)
            {
                if (skillsToSacrifice == 0)
                {
                    SkillsToBuyText.text = "You can continue";
                }
                else
                {
                    SkillsToBuyText.text = "Number of skills to sacrifice: <b>" + skillsToSacrifice.ToString() + "</b>";
                }
            }
            else 
            {
                SkillsToBuyText.text = "Just do what you want, you cheater";
            }
        }

        bool IsContinueButtonInteractable()
        { 
            if(CurrentMode == SkillTree.Mode.Build && skillsToBuy == 0)
            {
                return true;
            }
            else if(CurrentMode == SkillTree.Mode.Sacrifice && skillsToSacrifice == 0)
            {
                return true;
            }
         
            else if (CurrentMode == SkillTree.Mode.Cheat)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        void SetSkillTreeText()
        {
            if (CurrentMode == SkillTree.Mode.Build)
            {
                SkillTreeText.text = BuildText;
            }
            else if (CurrentMode == SkillTree.Mode.Sacrifice)
            {
                SkillTreeText.text = SacrificeText;
            }
            else if (CurrentMode == SkillTree.Mode.Cheat)
            {
                SkillTreeText.text = CheatText;
            }
        }


        
    }


    

}

