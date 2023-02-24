public class AttackState : BaseState<BasicAIController>
{
    public AttackState(FSM<BasicAIController> fsm) : base(fsm)
    {}

    public override void Update(float dt)
    {
        Owner.Attack();
    }
}