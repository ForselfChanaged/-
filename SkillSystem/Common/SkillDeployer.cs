using System.Collections.Generic;
using UnityEngine;


    public abstract class SkillDeployer:MonoBehaviour
    {
        private ISelector selector;
        private List<IImpact> impacts;
        protected SkillData skillData;
        public SkillData SkillData
        {
            get
            {
                return skillData;
            }
            set
            {
                skillData = value;
                InitDeployer();
            }
        }
        protected void InitDeployer()
        {
            //创建选取算法对象
            selector = DeployerConfigFactory.CreateISelector(SkillData);
            //创建影响效果对象
            impacts = new List<IImpact>();
            impacts = DeployerConfigFactory.CreateIImpact(SkillData, impacts);
        }
        //获取影响目标
        public void CalculateTargets()
        {
           
             SkillData.targets = selector.SelectorTarget(SkillData, transform);
        }
       
        protected void ExecuteCharacter()
        {
            foreach (var impact in impacts)
            {
                impact.Execute(this);
            }
        }
        public void OnSelectDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, skillData.attackDistance);
        }
        
        public abstract void DeploySkill();

    }


