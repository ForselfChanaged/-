
class MomentDamage : IImpact
{
    public void Execute(SkillDeployer deployer)
    {
        SkillData data = deployer.SkillData;
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

