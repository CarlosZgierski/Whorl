#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace EditorMethods {

    public class SceneEditorSetup
    {
        [MenuItem("Project Tools/SceneSetup")]
        static void CreateAllEmptyGameObjects()
        {
            CreateEmptyObjectWithName("------------Dynamic------------");
            CreateEmptyObjectWithName("-----------Managment-----------");
            CreateEmptyObjectWithName("--------------UI--------------");
            CreateEmptyObjectWithName("------------Cameras------------");
            CreateEmptyObjectWithName("------------Lights------------");
            CreateEmptyObjectWithName("------------World------------");
            CreateEmptyObjectWithName("------------Terrain------------");
            CreateEmptyObjectWithName("------------Props------------");
        }

        static void CreateEmptyObjectWithName(string x)
        {
            GameObject go = new GameObject(x);
            go.transform.position = new Vector3(0, 0, 0);
        }
    }

 
}
#endif