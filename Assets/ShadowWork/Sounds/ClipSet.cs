using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ClipSet")]
public class ClipSet : ScriptableObject
{


    [SerializeField]
    private Clip[] Clips;



    public Clip GetRandomClip()
    {
        int x = Random.Range(0,Clips.Length);
        return Clips[x];
    
    }


 


#if UNITY_EDITOR
    [TextArea]
    [SerializeField]
    private string DeveloperNote;

#endif



}

[System.Serializable]
public class Clip
{
    public AudioClip AudioClip;
    [Range(0, 1)]
    public float volume = 1f;

}
