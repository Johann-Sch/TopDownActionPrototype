using UnityEngine;

public class BasicAIController : AIController
{
    public Enemy Enemy { get; protected set; }
    
    [field: SerializeField] public Player Target { get; protected set; }

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();

        Enemy = Character.GetComponent<Enemy>();

        if (!Target)
            Target = GameManager.Instance.PlayerController.Player;
    }
    
    protected virtual void OnEnable()
    {
        Enemy.CooldownedLife.OnDeath += Die;
    }

    protected virtual void OnDisable()
    {
        Enemy.CooldownedLife.OnDeath -= Die;
    }
    #endregion Unity Functions
    
    #region BasicAIController
    public void MoveToTarget()
    {
        Enemy.MoveTo(Target.transform.position);
    }
    
    public void StandStill()
    {
        Enemy.StandStill();
    }

    public void Attack()
    {
        Character.Attack();
    }

    public void Die()
    {
        Enemy.Die();
        gameObject.SetActive(false);
    }
    #endregion BasicAIController
}
