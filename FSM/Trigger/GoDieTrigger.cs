namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public class GoDieTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return (fsm.ch_Status.HP <= 0);
        }

        public override void Init()
        {
            triggerID = TriggerID.GoDie;
        }
    }

}



