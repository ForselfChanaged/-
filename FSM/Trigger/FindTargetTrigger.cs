using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM
{
    class FindTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.target != null;
        }

        public override void Init()
        {
            triggerID = TriggerID.FindTarget;
        }
    }
}
