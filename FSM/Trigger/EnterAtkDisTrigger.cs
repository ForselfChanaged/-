

using UnityEngine;

namespace FSM
{
    class EnterAtkDisTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            if (fsm.target == null) return false;
            return fsm.ch_Status.atkDis > Vector3.Distance(fsm.target.position, fsm.transform.position);
        }

        public override void Init()
        {
            triggerID = TriggerID.EnterAtkDis;
        }
    }
}
