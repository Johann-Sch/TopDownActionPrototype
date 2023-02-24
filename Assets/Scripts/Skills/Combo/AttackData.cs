using UnityEngine;


// Inherit from this class and add data type and behaviour to specialize the attack
// Visual, collider type and size, sound...
[CreateAssetMenu(menuName = "CooldownedSkill/AttackData")]
public class AttackData : ScriptableObject
{
    [field: SerializeField] public float Damage { get; protected set; } = 1f;
    [field: SerializeField] public float MaxDelay { get; protected set; } = 1f;
    [field: SerializeField] public float Cooldown { get; protected set; } = 1f;

    public virtual void AttackBehaviour(ComboDelay comboDelay)
    {
        AttackCollider attackCollider = comboDelay.Character.AttackCollider;

        attackCollider.damage = Damage;
        attackCollider.gameObject.SetActive(true);
    }
}
