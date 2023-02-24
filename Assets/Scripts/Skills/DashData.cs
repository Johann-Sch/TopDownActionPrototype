using UnityEngine;

[CreateAssetMenu(menuName = "CooldownedSkill/DashData")]
public class DashData : CooldownedActiveSkillData
{
    [field: SerializeField] public AnimationCurve Curve { get; protected set; }
    [field: SerializeField] public float Distance { get; protected set; }
}