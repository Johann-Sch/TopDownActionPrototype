using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    #region Variables
    public Player Player { get; protected set; }
    public Vector2 InputVector { get; private set; }
    #endregion Variables
    
    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();

        Player = Character.GetComponent<Player>();
        GameManager.Instance.SetPlayerController(this);
    }
    
    protected virtual void FixedUpdate()
    {
        if (InputVector != Vector2.zero)
            Player.Move(InputVector);
    }
    #endregion Unity Functions
    
    #region PlayerController
    public void Move(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
    }

    public void MovementAction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            Player.MovementAction();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            Player.Attack();
    }
    #endregion PlayerController
}