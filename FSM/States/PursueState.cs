

namespace FSM
{
    class PursueState : FSMState
    {
        public override void Init()
        {
            stateID = StateID.PURSUE;
        }
        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.ani.SetBool("run", true);
            fsm.nav.isStopped = false;
        }
        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            fsm.MoveTarget(fsm.target.position, fsm.ch_Status.atkDis, fsm.ch_Status.speed);
        }
        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.ani.SetBool("run", false);
            fsm.nav.isStopped = true;
        }
    }
}
