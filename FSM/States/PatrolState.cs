
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FSM
{
    class PatrolState : FSMState
    {
        Vector3[] points;
        int index = 0;
        public override void Init()
        {
            stateID = StateID.PATROL;
        }
        public override void EnterState(FSMBase fsm)
        {
            base.ActionState(fsm);
            if(points==null||points.Length==0)
            {
                points = new Vector3[fsm.patrolPoint.Length];
                for (int i = 0; i < points.Length; i++)
                {
                    points[i] = fsm.patrolPoint[i];
                }
            }
            fsm.nav.isStopped = false;
            fsm.ani.SetBool("run", true);
        }
        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.ani.SetBool("run", false);
        }
        public void Loop(FSMBase fsm)
        {
           if(Vector3.Distance(points[index],fsm.transform.position)<0.2f)
            {
                index = (++index) % points.Length;
            }
            fsm.MoveTarget(points[index], 0, fsm.ch_Status.speed);
        }
        public void PingPong(FSMBase fsm)
        {
            if (Vector3.Distance(points[index], fsm.transform.position) < 0.2f)
            {
                index = (++index) % points.Length;
                if(index==points.Length-1)
                {
                    Array.Reverse(points);
                }
            }
            fsm.MoveTarget(points[index], 0, fsm.ch_Status.speed);
        }
        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            switch (fsm.patrolModle)
            {
                case PatrolModle.ONCE:break;
                case PatrolModle.LOOP: Loop(fsm); break;
                case PatrolModle.PINGPONG:PingPong(fsm); break;
              
            }
        }
    }
}
