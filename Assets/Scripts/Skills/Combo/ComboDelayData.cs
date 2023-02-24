using UnityEngine;

[CreateAssetMenu(menuName = "CooldownedSkill/ComboDelayData")]
public class ComboDelayData : CooldownedActiveSkillData
{
    [field: SerializeField] public AttackData[] Attack { get; protected set; }
}
