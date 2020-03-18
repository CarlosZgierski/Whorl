#if UNITY_EDITOR
using UnityEngine;
using System;
using UnityEditor;

namespace EditorMethods {
    static public class ScriptableObjectEditorMethods
    {

        [MenuItem("MyMenu/RefreshAllMasterLists")]
        public static void RefreshAllMasterLists()
        {

            MasterListOfSkills.Instance.RefreshList();



        }

        public static T[] GetAllInstancesOfScriptableObjectType<T>() where T : ScriptableObject
        {


            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;


        }

        public static int GetNumberOfInstancesOfScriptableObjectType<T>() where T : ScriptableObject
        {
            return AssetDatabase.FindAssets("t:" + typeof(T).Name).Length;
        }
    }
    
}
#endif


