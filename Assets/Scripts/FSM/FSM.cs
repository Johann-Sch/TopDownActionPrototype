using UnityEngine;

public abstract class FSM<TOwner> : MonoBehaviour where TOwner : Controller
{
    public BaseState<TOwner> CurrentState { get; protected set; }
    
    [field: SerializeField] public TOwner Owner { get; protected set; }

    protected virtual void Awake()
    {
        if (!Owner)
        {
            Owner = GetComponent<TOwner>();

            if (!Owner)
            {
                Debug.LogError("FSM is missing Controller.");

                enabled = false;
            }
        }
    }
    
    protected virtual void Start()
    {
        CurrentState = GetInitialState();

        if (CurrentState != null)
            CurrentState.Enter();
        else
        {
            Debug.LogError("FSM is missing InitialState.");

            enabled = false;
        }
    }

    protected virtual void Update()
    {
        if (!CurrentState.CheckTransitions())
            CurrentState.Update(Time.deltaTime);
    }

    public void ChangeState(BaseState<TOwner> newState)
    {
        CurrentState.Exit();

        CurrentState = newState;

        CurrentState.Enter();
    }

    protected abstract BaseState<TOwner> GetInitialState();
}