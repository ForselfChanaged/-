

namespace FSM
{
    class IdelState : FSMState
    {
        public override void Init()
        {
            stateID = StateID.IDEL;
        }
        public override void EnterState(FSMBase fsm)
        {
            fsm.ani.SetBool("idle", true);
        }
        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.ani.SetBool("idle", false);
        }
    }
}
