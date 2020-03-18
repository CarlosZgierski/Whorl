#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using EditorMethods;


[InitializeOnLoad]
public class OnLoadEditorMethods : MonoBehaviour
{
    static OnLoadEditorMethods()
    {
        ScriptableObjectEditorMethods.RefreshAllMasterLists();
    }

}
#endif













