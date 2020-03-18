
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameObjectVariable")]
public class GameObjectVariable : ScriptableObject
{
    [SerializeField]
    [Multiline]
    private string DeveloperDescription;
    [Space]
    [SerializeField]
    protected GameObject value;

    public virtual GameObject Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
        }
    }
    
    public static implicit operator GameObject(GameObjectVariable reference)
    {
        return reference.Value;
    }
}
