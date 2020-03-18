using System.Linq;
using UnityEngine;
using EditorMethods;



public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    static T _instance = null;

    public static T Instance
    {

        get
        {
#if UNITY_EDITOR
            if (!_instance)
                {
                    if (ScriptableObjectEditorMethods.GetNumberOfInstancesOfScriptableObjectType<T>() == 1)
                    {
                        _instance = ScriptableObjectEditorMethods.GetAllInstancesOfScriptableObjectType<T>()[0];
                    }
                    else
                    {
                        if (ScriptableObjectEditorMethods.GetNumberOfInstancesOfScriptableObjectType<T>() > 1)
                        {
                            Debug.LogError("There's more than one instance of SingletonScriptableObject: " + typeof(T).Name);
                            _instance = ScriptableObjectEditorMethods.GetAllInstancesOfScriptableObjectType<T>()[0];
                        }
                        else
                        {
                            Debug.LogError("There's no instance of SingletonScriptableObject: " + typeof(T).Name);
                        }
                    }
                }
#endif
            return _instance;
      
        }
    }

#if UNITY_EDITOR
    private static bool CheckIfOnlyInstance()
    {

        if (ScriptableObjectEditorMethods.GetNumberOfInstancesOfScriptableObjectType<T>() == 1)
        {
            return true;
        }

        else
        {
            return false;
        }

    }

#endif
}