using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class VariationController : MonoBehaviour
{
    [SerializeField]
    public Variation[] variations;


    void OnEnable()
    {
        DisableAllVariations();
        ChooseVariation().SetActive(true);
       

    }

    void DisableAllVariations()
    {
        foreach(Variation x in variations)
        {
            x.GameObjectRoot.SetActive(false);
        }
    }

    public GameObject ChooseVariation()
    {
        
        float x = UnityEngine.Random.Range(0f, 1f);
        float amountReached = 0;
        for(int i = 0; i < variations.Length; i++)
        {
            if(x <= variations[i].Odds + amountReached)
            {
                return (variations[i].GameObjectRoot);
            }
            else
            {
                amountReached += variations[i].Odds;
            }
        }
        Debug.LogWarning("Variation choice in " + gameObject.name + " not working properly. Check variations odds");
        
        return variations[0].GameObjectRoot;
    }

 
}

[System.Serializable]
public class Variation
{ 
    public GameObject GameObjectRoot;
    public float Odds;
}



#if UNITY_EDITOR
[CustomEditor(typeof(VariationController))]
class VariationControllerHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
   
        if (GUILayout.Button("Random variation", GUILayout.Width(300), GUILayout.Height(50)))
        {
            VariationController variationController = target as VariationController;
            foreach (Variation x in variationController.variations)
            {
                x.GameObjectRoot.SetActive(false);
            }
            variationController.ChooseVariation().SetActive(true);
        }
        if (GUILayout.Button("Equalize odds", GUILayout.Width(300), GUILayout.Height(50)))
        {
            VariationController variationController = target as VariationController;
            Debug.Log(variationController.variations.Length);
            float odd = 1f / variationController.variations.Length;
            Debug.Log(odd.ToString());
            foreach (Variation x in variationController.variations)
            {
                x.Odds = odd;
            }
            variationController.ChooseVariation().SetActive(true);
        }

    }
}
#endif

