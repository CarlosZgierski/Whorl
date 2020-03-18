using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ListOfObjects<T> : ScriptableObject
{
    public List<T> list;

}
