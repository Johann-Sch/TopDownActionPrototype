using UnityEngine;

[CreateAssetMenu(menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    [field: SerializeField] public float Speed { get; protected set; } = 10f;
    [field: SerializeField] public float Gravity { get; protected set; } = -9.81f;
    [field: SerializeField] public float MaxLife { get; protected set; } = 3;
}