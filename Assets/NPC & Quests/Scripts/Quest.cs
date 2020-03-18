
using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ScriptableObjects/NPC and Quest/Quest")]
public class Quest : ScriptableObject
{
    public Stage[] Stages;

 

    [Serializable]
    public class Stage
    {
        public string name;
        public List<Dialogue> Dialogues;
    }
}
