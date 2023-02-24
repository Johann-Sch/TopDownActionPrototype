using UnityEngine;

public class BasicAIFSM : FSM<BasicAIController>
{
    public Player Target => Owner.Target;
    public Enemy Self => Owner.Enemy;
    
    public IdleState IdleState { get; protected set; }
    public ChaseState ChaseState { get; protected set; }
    public AttackState AttackState { get; protected set; }

    [field: SerializeField] public AIFSMData Data { get; protected set; }

    private float m_chaseSqrRange;

    protected override void Awake()
    {
        base.Awake();

        if (!Data)
        {
            Debug.LogError("BasicAIFSM is missing AIFSMData.");

            enabled = false;
        }

        IdleState = new IdleState(this);
        ChaseState = new ChaseState(this);
        AttackState = new AttackState(this);

        m_chaseSqrRange = Data.ChaseRange * Data.ChaseRange;
    }
    
    protected override void Start()
    {
        base.Start();
        
        CreateTransitions();
    }

    protected virtual void CreateTransitions() // override to change transitions
    {
        // From Iddle transitions
        IdleState.AddTransition(ChaseState,
            () => Vector3.SqrMagnitude(Target.transform.position - Self.transform.position) <= m_chaseSqrRange);
        
        float attackColliderRange = (Self.transform.position - Self.AttackCollider.transform.position).magnitude;
        attackColliderRange += Self.AttackCollider.GetComponent<Collider>().bounds.extents.z;
        attackColliderRange += Target.GetComponent<Collider>().bounds.extents.z;
        if (Data.AttackRange >= attackColliderRange)
            Debug.LogWarning("AI attack range is too high to reach target.");
        if (Data.AttackRange <= Self.GetComponent<Collider>().bounds.extents.z + Target.GetComponent<Collider>().bounds.extents.z )
            Debug.LogWarning("AI attack range is too low to reach target.");

        // From Chase transitions
        ChaseState.AddTransition(AttackState, delegate
        {
            float dist = Vector3.Distance(Target.transform.position, Self.transform.position);

            if (dist <= Data.AttackRange
                && Physics.Raycast(Self.transform.position, Self.transform.forward, dist, LayerMask.NameToLayer("Player")))
                return true;

            return false;
        });
        ChaseState.AddTransition(IdleState,
            () => Vector3.SqrMagnitude(Target.transform.position - Self.transform.position) > m_chaseSqrRange);
        
        // From Attack transitions
        AttackState.AddTransition(ChaseState,delegate
        {
            float dist = Vector3.Distance(Target.transform.position, Self.transform.position);

            if (dist > Data.AttackRange
                || !Physics.Raycast(Self.transform.position, Self.transform.forward, dist, LayerMask.NameToLayer("Player")))
                return true;

            return false;
        });
    }

    protected override BaseState<BasicAIController> GetInitialState()
    {
        return IdleState;
    }
}
