using UnityEngine.Events;

public class CooldownedLife : CooldownedSkill<CooldownedSkillData>
{
    #region Variables
    public float Life { get; protected set; }
    public bool Dead => Life <= 0f;

    #endregion Variables
    
    public UnityAction OnDeath;
    public UnityAction OnLifeChange;
    public UnityAction OnHurt;

    #region CooldownedLife
    public void ResetLife()
    {
        Life = Character.Data.MaxLife;
        
        OnLifeChange?.Invoke();
    }

    public void TryDamage(float damage = 1f)
    {
        if (damage <= 0f)
            return;
        
        if (!OnCooldown && !Dead)
        {
            Life -= damage;
            
            UseSkill();
            OnHurt?.Invoke();
        }
    }
    
    protected override void SkillBehaviour()
    {
        if (Life <= 0)
            OnDeath?.Invoke();
            
        OnLifeChange?.Invoke();
    }
    #endregion CooldownedLife
}