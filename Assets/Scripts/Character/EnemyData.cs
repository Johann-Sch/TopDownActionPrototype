using UnityEngine;

[CreateAssetMenu(menuName = "Character/EnemyData")]
public class EnemyData : CharacterData
{
    [field: SerializeField] public float AngularSpeed { get; protected set; } = 120f;
}