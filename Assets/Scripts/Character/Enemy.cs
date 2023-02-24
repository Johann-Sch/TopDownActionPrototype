using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Character
{
    public NavMeshAgent NavMeshAgent { get; protected set; }
    public EnemyData EnemyData { get; protected set; }
    
    // //// TMP \\\\
    private float duration = .1f;
    private float timeActive = 0f;

    void Update()
    {
        timeActive += Time.deltaTime;

        if (timeActive >= duration)
        {
            timeActive = 0f;
            AttackCollider.gameObject.SetActive(false);
        }
    }
    // \\\\ TMP ////

    protected override void Awake()
    {
        base.Awake();

        EnemyData = Data as EnemyData;
        
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = Data.Speed;
        NavMeshAgent.angularSpeed = EnemyData.AngularSpeed;
    }

    #region Enemy
    public void MoveTo(Vector3 target)
    {
        NavMeshAgent.destination = target;
        
        CharacterController.Move(NavMeshAgent.desiredVelocity.normalized * Data.Speed * Time.deltaTime);
    }

    public void StandStill()
    {
        NavMeshAgent.isStopped = true;
        NavMeshAgent.ResetPath();
    }

    public void Die()
    {
        
    }
    #endregion Enemey
}