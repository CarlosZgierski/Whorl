

using UnityEngine;
using System.Collections.Generic;
using EditorMethods;

//[CreateAssetMenu]
public abstract class MasterListOfScriptableObjectType<T, C> : SingletonScriptableObject<T> where T: ScriptableObject where C: ScriptableObject
{
    [SerializeField]
    protected List<C> list;

    public List<C> List
    {
        get
        {
            return list;
        }
    }




    public void RefreshList()
    {
#if UNITY_EDITOR
        list.Clear();
        C[] array = ScriptableObjectEditorMethods.GetAllInstancesOfScriptableObjectType<C>();
        foreach(C x in array)
        {
            list.Add(x);
        }
#endif
    }

}


