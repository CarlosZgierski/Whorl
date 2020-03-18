
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CharacterStats")]
public class CharacterStats : ScriptableObject
{

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float dodgeSpeed;


    public float MaxHealth { get { return maxHealth; } }
    public float Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float RotationSpeed { get { return rotationSpeed; } }
    public float DodgeSpeed { get { return dodgeSpeed; } }
}
