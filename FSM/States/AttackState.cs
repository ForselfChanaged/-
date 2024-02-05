
using UnityEngine;

namespace FSM
{
    class AttackState : FSMState
    {
        private float attackTime;
        public override void Init()
        {
            stateID = StateID.ATTACK;
        }
        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.nav.isStopped = true;
            fsm.ani.SetBool("run", false);
        }
        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);

            if (attackTime < Time.time)
            {
                fsm.LookAtTarget(fsm.target.position, 1);
                attackTime = Time.time + fsm.ch_Status.atkInternal;
                fsm.ani.SetBool("attack", true);
                fsm.target.GetComponent<CharacterStatus>().Damage(fsm.ch_Status.atk);
            }

        }
        public override void ExitState(FSMBase fsm)
        {
            fsm.ani.SetBool("attack", false);
            base.ExitState(fsm);
            fsm.nav.isStopped = false;

        }

    }
}
