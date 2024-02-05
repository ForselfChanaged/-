using System.Collections;
using UnityEngine;

class Damage : IImpact
{
    SkillData data;
    public void Execute(SkillDeployer deployer)
    {
        data = deployer.SkillData;

        deployer.StartCoroutine(PersistDamage(deployer));
    }
    IEnumerator PersistDamage(SkillDeployer deployer)
    {
        float time = 0;
        do
        {
            deployer.CalculateTargets();
            OnceDamage();
            yield return new WaitForSeconds(data.atkInterval);
            time += data.atkInterval;
        } while (time < data.durationTime);

    }
    private void OnceDamage()
    {
        foreach (var target in data.targets)
        {
            float damage = data.atkRatio * data.owner.GetComponent<CharacterStatus>().atk;
            target.GetComponent<CharacterStatus>().Damage(damage);
            var ex = ObjectPool.Instance.GetGameObject(data.hitFixPerfab.name, data.hitFixPerfab);
            ex.transform.position = target.position;
            ObjectPool.Instance.DelayRecycle(data.hitFixPerfab.name, ex, 1);
        }
    }
}

