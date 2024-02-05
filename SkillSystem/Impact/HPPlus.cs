
using System.Collections;
using UnityEngine;
class HPPlus : IImpact
{
    SkillData data;
    public void Execute(SkillDeployer deployer)
    {
        data = deployer.SkillData;
        deployer.StartCoroutine(PersistPlus(deployer));
    }
    IEnumerator PersistPlus(SkillDeployer deployer)
    {
        float time = 0;
        do
        {
            deployer.CalculateTargets();
            OnceAddHp(5);
            yield return new WaitForSeconds(data.atkInterval);
            time += data.atkInterval;
        } while (time < data.durationTime);
    }
    public void OnceAddHp(float value)
    {
        foreach (var target in data.targets)
        {
            target.GetComponent<CharacterStatus>().AddHP(value);
        }
    }

}
