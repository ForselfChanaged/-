
using UnityEngine;

namespace SkillSystem 
{
    class MeleeSkillDeployer : SkillDeployer
    {
        private void OnDrawGizmos()
        {
           // Gizmos.DrawWireSphere(transform.position, SkillData.attackDistance);
        }
        public override void DeploySkill()
        {
             CalculateTargets();
             ExecuteCharacter();
        }
    }
}
