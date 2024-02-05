class CostSP : IImpact
{
    public void Execute(SkillDeployer deployer)
    {
        deployer.SkillData.owner.GetComponent<CharacterStatus>().SP -= deployer.SkillData.costSP;
    }
}

