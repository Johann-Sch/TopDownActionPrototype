using UnityEngine;
using UnityEngine.Events;

public class ComboDelay : CooldownedActiveSkill<ComboDelayData>
{
    #region Variables
    public UnityAction OnComboComplete;
    
    protected int m_currentCombo = 0;
    #endregion Variables

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();

        if (Data.Attack.Length <= 0)
            enabled = false;
    }

    protected virtual void OnEnable()
    {
        OnCooldownFinished += FinishAttack;
    }

    protected virtual void OnDisable()
    {
        OnCooldownFinished -= FinishAttack;
    }
    #endregion Unity Functions
    
    private void FinishAttack()
    {
        if (!OnCooldown)
            Character.AttackCollider.gameObject.SetActive(false);
        Destroy(Character.AttackCollider.GetComponent<Collider>());
    }

    #region ComboDelay

    public override void UseSkill()
    {
        if (!OnCooldown)
        {
            Data.Duration = Data.Attack[m_currentCombo].MaxDelay;
            Data.Cooldown = Data.Attack[m_currentCombo].Cooldown;
        }

        base.UseSkill();
    }

    protected override void SkillBehaviour()
    {
        if (!IsActive)
            m_currentCombo = 0;

        base.SkillBehaviour();
        
        NextCombo();
    }

    protected void NextCombo()
    {
        Data.Attack[m_currentCombo].AttackBehaviour(this);
        m_currentCombo++;

        if (m_currentCombo >= Data.Attack.Length)
        {
            OnComboComplete?.Invoke();
            
            m_currentCombo = 0;
        }
    }
    #endregion
}