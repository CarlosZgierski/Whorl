using UnityEditor;
using UnityEngine;

public class EditorGameObjectMenu : MonoBehaviour
{


    [MenuItem("GameObject/Encounter", false, 20)]
    static void CreateEncounter()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Encounter.prefab", typeof(GameObject)) as GameObject;
        GameObject x = Instantiate(prefab, SceneView.lastActiveSceneView.pivot, Quaternion.identity);
        Selection.activeTransform = x.transform;
     
    }

    void OldWay()
    {
        GameObject encounterGameObject = new GameObject("Encounter");
        Encounter encounterComponent = encounterGameObject.AddComponent<Encounter>();
        /*if (Selection.activeTransform != null)
        {
            encounterGameObject.transform.SetParent(Selection.activeTransform, false);
            encounterGameObject.transform.position = Selection.activeTransform.position;
        }
       
        else
        {
         */
        encounterGameObject.transform.position = SceneView.lastActiveSceneView.pivot;
        //}
        Selection.activeTransform = encounterGameObject.transform;

        GameObject TriggersParent = new GameObject("EncounterTriggers");
        GameObject WallsParent = new GameObject("EncounterWalls");
        GameObject EnemiesParent = new GameObject("EncounterEnemies");

        TriggersParent.transform.parent = encounterGameObject.transform;
        WallsParent.transform.parent = encounterGameObject.transform;
        EnemiesParent.transform.parent = encounterGameObject.transform;

        encounterComponent.TriggersParent = TriggersParent;
        encounterComponent.WallsParent = WallsParent;
        encounterComponent.EnemiesParent = EnemiesParent;

        TriggersParent.transform.localPosition = Vector3.zero;
        WallsParent.transform.localPosition = Vector3.zero;
        EnemiesParent.transform.localPosition = Vector3.zero;


        GameObject ExempleTrigger = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ExempleTrigger.transform.parent = TriggersParent.transform;
        EncounterTrigger ExampleTriggerComponent = ExempleTrigger.AddComponent<EncounterTrigger>();
        ExampleTriggerComponent.EncounterToTrigger = encounterComponent;
        ExempleTrigger.GetComponent<MeshRenderer>().material = null;
        ExempleTrigger.GetComponent<BoxCollider>().isTrigger = true;
        ExempleTrigger.layer = LayerMask.NameToLayer("Trigger");
        ExempleTrigger.transform.localPosition = new Vector3(0, 0, -2f);

        //Não terminado
        WallsParent.SetActive(false);



    }
}
