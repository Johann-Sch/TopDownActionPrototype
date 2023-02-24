using UnityEngine;

[CreateAssetMenu(menuName = "CooldownedSkill/CooldownedSkillData")]
public class CooldownedSkillData : ScriptableObject
{
    [field: SerializeField] public float Cooldown { get; set; } = 1f;
}