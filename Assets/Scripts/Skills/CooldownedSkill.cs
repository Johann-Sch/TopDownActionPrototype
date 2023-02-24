using UnityEngine;
using UnityEngine.Events;

public abstract class CooldownedSkill<T> : MonoBehaviour where T : CooldownedSkillData
{
    #region Variables
    [field: SerializeField] public Character Character { get; protected set; }
    [field: SerializeField] public T Data { get; protected set; }

    public UnityAction OnSkillUse;
    public UnityAction OnCooldownFinished;
    public float RemainingDelay { get; protected set; }
    public bool OnCooldown { get; protected set; }
    #endregion Variables
    
    #region Unity Functions
    protected virtual void Awake()
    {
        if (!Character)
        {
            Character = GetComponent<Character>();

            if (!Character)
            {
                Debug.LogError("CooldownedSkill is missing Character.");

                enabled = false;
            }
        }
        
        if (!Data)
        {
            Debug.LogError("CooldownedSkill is missing Data.");

            enabled = false;
        }
    }
    
    protected virtual void Update()
    {
        if (OnCooldown)
        {
            RemainingDelay -= Time.deltaTime;

            if (RemainingDelay <= 0f)
            {
                OnCooldown = false;
                
                OnCooldownFinished?.Invoke();
            }
        }
    }
    #endregion Unity Functions
    
    #region CooldownedSkill
    public virtual void UseSkill()
    {
        if (!OnCooldown)
        {
            OnCooldown = true;
            RemainingDelay = Data.Cooldown;
            OnSkillUse?.Invoke();

            SkillBehaviour();
        }
    }

    protected abstract void SkillBehaviour();

    #endregion CooldownedSkill
}
