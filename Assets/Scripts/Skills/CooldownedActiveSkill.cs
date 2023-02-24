using UnityEngine;
using UnityEngine.Events;

public class CooldownedActiveSkill<T> : CooldownedSkill<T> where T : CooldownedActiveSkillData
{
    #region Variables
    public UnityAction OnSkillCompleted;
    public float CurrentTime { get; protected set; }
    public bool IsActive { get; protected set; }
    #endregion Variables

    protected virtual void FixedUpdate()
    {        
        if (IsActive)
        {
            CurrentTime += Time.deltaTime;

            if (CurrentTime >= Data.Duration)
            {
                IsActive = false;
                
                OnSkillCompleted?.Invoke();
            }
        }
    }
    
    #region CooldownedActiveSkill
    protected override void SkillBehaviour()
    {
        IsActive = true;
        CurrentTime = 0f;
    }
    #endregion CooldownedActiveSkill
}
