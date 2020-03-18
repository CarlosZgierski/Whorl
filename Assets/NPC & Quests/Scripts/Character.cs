using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/NPC and Quest/Character")]
public class Character : ScriptableObject
{
    public string name;
    public GameObject prefab;

    public Quest[] IntroductionQuests;
    public Dialogue[] observationDialogues;
    public bool instantiated = false;
    //Jeito de checar se foi instantiato já pra não fazer isso duas vezes
}
