using UnityEngine;

public class Dash : CooldownedActiveSkill<DashData>
{
    #region Variables
    private float m_distanceRatio;
    #endregion Variables
    
    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();

        m_distanceRatio = Data.Distance / Data.Duration;
    }

    protected override void FixedUpdate()
    {
        if (IsActive)
        {
            float currentTimeRatio = CurrentTime / Data.Duration;
            Vector3 move = Character.transform.forward * Data.Curve.Evaluate(currentTimeRatio) * m_distanceRatio;

            float dt = Mathf.Clamp(Time.deltaTime, 0f, Data.Duration - CurrentTime);

            Character.CharacterController.Move(move * dt);
        }

        base.FixedUpdate();
    }
    #endregion Unity Functions
}