using System;
using System.Collections.Generic;

namespace FSM
{
    ///<summary>
    ///所有状态类的基类
    ///状态含有条件列表
    ///每个状态应该有条件映射表，当其中条件满足根据映射表返回的ID切换条件
    ///子类状态选则性实现EnterState,ActionState,ExitState
    ///</summary>
    public abstract class FSMState
    {
        public StateID stateID;
        public List<FSMTrigger> triggerList;
        public Dictionary<TriggerID, StateID> map;
        public FSMState()
        {
            triggerList = new List<FSMTrigger>();//生成条件列表
            map = new Dictionary<TriggerID, StateID>();//生成映射表
            Init();
        }
        public abstract void Init();
        public virtual void EnterState(FSMBase fsm) { }
        public virtual void ActionState(FSMBase fsm) { }
        public virtual void ExitState(FSMBase fsm) { }
        public void AddMap(TriggerID triggerID,StateID stateID)
        {
            map.Add(triggerID, stateID);
            //使用反射动态创建
            Type t = Type.GetType("FSM." + triggerID + "Trigger");
            FSMTrigger trigger = Activator.CreateInstance(t) as FSMTrigger;
            triggerList.Add(trigger);
        }
        public void Reason(FSMBase fsm)
        {
            if (triggerList == null) return;
            foreach (var trigger in triggerList)
            {
                if(trigger.HandleTrigger(fsm))
                {
                    StateID targetID = map[trigger.triggerID];
                    fsm.TranslateState(targetID);
                    return;
                }
            }
        }
    }
}
  

  
