using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController), typeof(CooldownedLife))]
public class Character : MonoBehaviour
{
    #region Variables
    public CharacterController CharacterController { get; private set; }
    public CooldownedLife CooldownedLife { get; protected set; }
    
    [field: SerializeField] public CharacterData Data { get; protected set; }
    [field: SerializeField] public AttackCollider AttackCollider { get; protected set; }
    [field: SerializeField] public Renderer Renderer { get; protected set; }

    private Color m_originalColor;
    private float m_yVelocity;
    #endregion Variables
    
    #region Unity Functions
    protected virtual void Awake()
    {
        if (!Data)
        {
            Debug.LogError("Character is missing CharacterData.");

            enabled = false;

            return;
        }

        if (!AttackCollider)
        {
            Debug.LogError("Character is missing AttackCollider.");

            enabled = false;

            return;
        }

        if (!Renderer)
        {
            Renderer = GetComponentInParent<Renderer>();

            if (!Renderer)
            {
                Debug.LogError("Character is missing Renderer.");

                enabled = false;

                return;
            }
        }

        m_originalColor = Renderer.material.color;
        CharacterController = GetComponent<CharacterController>();
        CooldownedLife = GetComponent<CooldownedLife>();
        
        CooldownedLife.ResetLife();
    }

    protected virtual void OnEnable()
    {
        CooldownedLife.OnHurt += Pain;
        CooldownedLife.OnCooldownFinished += Heal;
    }

    protected virtual void OnDisable()
    {
        CooldownedLife.OnHurt -= Pain;
        CooldownedLife.OnCooldownFinished -= Heal;
    }
    
    protected virtual void FixedUpdate()
    {
        ApplyGravity();
    }
    #endregion Unity Functions
    
    #region Character
    public void Move(Vector2 inputVector)
    {
        Vector3 movement = new Vector3(inputVector.x, 0f, inputVector.y);
        
        CharacterController.Move(movement * Time.fixedDeltaTime * Data.Speed);
        transform.forward = movement;
    }

    public virtual void ApplyGravity()
    {
        m_yVelocity = CharacterController.isGrounded ? 0f : m_yVelocity + Data.Gravity * Time.deltaTime;

        if (!CharacterController.isGrounded)
            CharacterController.Move(new Vector3(0f, m_yVelocity * Time.deltaTime, 0f));
    }

    public virtual void Attack()
    {
        AttackCollider.gameObject.SetActive(true);
    }

    public void Pain()
    {
        Renderer.material.color = Color.white;
    }

    public void Heal()
    {
        Renderer.material.color = m_originalColor;
    }

    public void Respawn()
    {
        CharacterController.enabled = false;
        CharacterController.transform.position = GameManager.Instance.SpawnPoint;
        CharacterController.enabled = true;
    }

    public void Revive()
    {
        CooldownedLife.ResetLife();
        Respawn();
    }
    #endregion Character
}