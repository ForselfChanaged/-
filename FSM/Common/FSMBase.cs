
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SkillSystem;
namespace FSM
{
    ///<summary>
    ///挂载到AI上的有限状态机，负责接受外部信息触发条件以及条件的切换
    ///</summary>
    public class FSMBase : MonoBehaviour
    {

        public StateID defaultStateID;
        public StateID actionStateID;
        [Header("巡逻模式")]
        public PatrolModle patrolModle;
        public Vector3[] patrolPoint;
        [Header("目标检测层")]
        public LayerMask targetMask;
        [Header("检测范围")]
        public float checkDistance = 8;
        public Transform target;


        private FSMState defaultState;
        private FSMState actionState;
        private List<FSMState> states;


        [HideInInspector]
        public CharacterStatus ch_Status;
        [HideInInspector]
        public NavMeshAgent nav;
        [HideInInspector]
        public Animator ani;
        //[HideInInspector]
        //public Vector3 home;
        /// <summary>
        /// 初始化获取组件
        /// </summary>
        private void InitComponet()
        {
            ch_Status = GetComponent<CharacterStatus>();
            nav = GetComponent<NavMeshAgent>();
            ani = GetComponentInChildren<Animator>();

        }
        #region 手动配置状态机
        private void ConfigFSM()
        {
            states = new List<FSMState>();
            FSMState idle = new IdelState();
            FSMState die = new DieState();
            FSMState pursue = new PursueState();
            FSMState attack = new AttackState();
            FSMState patrol = new PatrolState();

            idle.AddMap(TriggerID.GoDie, StateID.DIE);
            idle.AddMap(TriggerID.FindTarget, StateID.PURSUE);
            idle.AddMap(TriggerID.EnterAtkDis, StateID.ATTACK);

            pursue.AddMap(TriggerID.GoDie, StateID.DIE);
            pursue.AddMap(TriggerID.LostTarget, StateID.PATROL);
            pursue.AddMap(TriggerID.EnterAtkDis, StateID.ATTACK);


            attack.AddMap(TriggerID.GoDie, StateID.DIE);
            attack.AddMap(TriggerID.ExitAtkDis, StateID.PURSUE);


            patrol.AddMap(TriggerID.GoDie, StateID.DIE);
            patrol.AddMap(TriggerID.FindTarget, StateID.PURSUE);
            patrol.AddMap(TriggerID.EnterAtkDis, StateID.ATTACK);


            states.Add(attack);
            states.Add(pursue);
            states.Add(idle);
            states.Add(die);
            states.Add(patrol);
        }
        #endregion

        /// <summary>
        /// 初始化默认状态
        /// </summary>
        private void InitDefaultState()
        {
            defaultState = states.Find(s => s.stateID == defaultStateID);
            actionState = defaultState;
            actionState.EnterState(this);
        }
        private void Start()
        {
            InitComponet();
            ConfigFSM();
            InitDefaultState();

        }
        private void Update()
        {
            actionStateID = actionState.stateID;
            actionState.Reason(this);
            actionState.ActionState(this);
            FindTarget();

        }
        public void TranslateState(StateID targetStateID)
        {
            actionState.ExitState(this);
            actionState = targetStateID == defaultStateID ? defaultState : states.Find(s => s.stateID == targetStateID);
            actionState.EnterState(this);

        }
        public void FindTarget()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkDistance, targetMask);
            if (colliders == null || colliders.Length == 0) target = null;
            else target = colliders[0].transform;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, checkDistance);
        }
        public void MoveTarget(Vector3 targetPos, float stopDis, float speed)
        {
            nav.SetDestination(targetPos);
            nav.stoppingDistance = stopDis;
            nav.speed = speed;
        }
        public void LookAtTarget(Vector3 pos, float rotatespeed = 0.6f)
        {
            Vector3 direction = new Vector3(pos.x - transform.position.x, 0, pos.z - transform.position.z);
            if (direction.Equals(Vector3.zero)) return;
            Quaternion quaternion = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, rotatespeed);
        }
    }

}



