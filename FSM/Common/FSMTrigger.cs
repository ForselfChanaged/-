

namespace FSM
{
    /// <summary>
    /// 所有条件类的基类，
    /// 由于再FSMState中我们利用反射来动态创建条件加载到其中的条件列表中，所以再命名条件类时应注意名称
    /// 
    /// </summary>
  public abstract class FSMTrigger
    {
        public TriggerID triggerID;
        public FSMTrigger()
        {
            Init();
        }
        public abstract void Init();
        public abstract bool HandleTrigger(FSMBase fsm);
       
    }
}
