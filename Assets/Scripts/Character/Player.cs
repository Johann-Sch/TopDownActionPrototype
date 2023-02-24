using UnityEngine;

[RequireComponent(typeof(Dash), typeof(ComboDelay))]
public class Player : Character
{
    public Dash Dash { get; protected set; }
    public ComboDelay ComboDelay { get; protected set; }
    public PlayerData PlayerData { get; protected set; }

    private bool m_tryAttack = false;
    
    protected override void Awake()
    {
        base.Awake();
        
        Dash = GetComponent<Dash>();
        ComboDelay = GetComponent<ComboDelay>();
        PlayerData = Data as PlayerData;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        ComboDelay.OnCooldownFinished += CheckStackedAttack;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        ComboDelay.OnCooldownFinished -= CheckStackedAttack;
    }

    public override void ApplyGravity()
    {
	// prevent Player from falling during dash
        if (!Dash.IsActive)
            base.ApplyGravity();
    }

    public override void Attack()
    {
        if (ComboDelay.OnCooldown)
            m_tryAttack = true;
        else
            ComboDelay.UseSkill();
    }

    public void MovementAction()
    {
        Dash.UseSkill();
    }

    private void CheckStackedAttack()
    {
        if (m_tryAttack)
        {
            m_tryAttack = false;
            Attack();
        }
    }
}
