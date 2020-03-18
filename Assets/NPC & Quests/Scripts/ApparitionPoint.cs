using System;
using UnityEngine;

public class ApparitionPoint : MonoBehaviour
{
    public ApparitionPossibility[] ApparitionPossibilities;


    void Start()
    {
        //se tiver uma quest prioridade ativa fazer com que a instantiação seja certa
        //Check if intantiateCharacter  
        foreach(ApparitionPossibility x in ApparitionPossibilities)
        {
            if(!x.Character.instantiated && UnityEngine.Random.Range(0f,1f) < x.odds)
            {
                InstantiateCharacterApparition(x);
                
                return;
            }
        }
        Destroy(gameObject);

    }

    void InstantiateCharacterApparition(ApparitionPossibility possibility)
    {
        Instantiate(possibility.Character.prefab, transform.position, transform.rotation);
        possibility.Character.instantiated = true;
    }


    [Serializable]
    public class ApparitionPossibility
    {
        public Character Character;
        public float odds = 0.5f;
       
    }
}
