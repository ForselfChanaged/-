

namespace FSM
{
    class LostTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
          return  fsm.target == null;
        }

        public override void Init()
        {
            triggerID = TriggerID.LostTarget;
        }
    }
}
