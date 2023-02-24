using UnityEngine;

[CreateAssetMenu(menuName = "CooldownedSkill/CooldownedActiveData")]
public class CooldownedActiveSkillData : CooldownedSkillData
{
    [field: SerializeField] public float Duration { get; set; }
}