using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public class DieState : FSMState
    {
        public override void Init()
        {
            stateID = StateID.DIE;
        }
        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.ani.SetBool("die", true);
       
        }
    
    }



}

