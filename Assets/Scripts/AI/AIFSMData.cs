using UnityEngine;

[CreateAssetMenu(menuName = "AI/AIFSMData")]
public class AIFSMData : ScriptableObject
{
    [field: SerializeField] public float ChaseRange { get; protected set; } = 15f;
    [field: SerializeField] public float AttackRange { get; protected set; } = 15f;
}
