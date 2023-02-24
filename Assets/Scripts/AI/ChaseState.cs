public class ChaseState : BaseState<BasicAIController>
{
    public ChaseState(FSM<BasicAIController> fsm) : base(fsm)
    {}

    public override void Update(float dt)
    {
        Owner.MoveToTarget();
    }

    public override void Exit()
    {
        Owner.StandStill();
    }
}