using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMgr : MonoBehaviour
{
    public List<SkillData> skills;
    private Animator ani;
    private CharacterStatus status;
    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        status = GetComponent<CharacterStatus>();
        foreach (var skill in skills)
        {
            skill.owner = transform.gameObject;
            skill.isCooling = false;
        }
    }
    public void Init(List<SkillData> _skills)
    {
        ani = GetComponentInChildren<Animator>();
        status = GetComponent<CharacterStatus>();
        skills = _skills;
        foreach (var skill in skills)
        {
            skill.owner = transform.gameObject;
            skill.isCooling = false;
        }
    }
    public void UseSkill(int id, Vector3 pos)
    {
        var skill = skills.Find(s => s.ID == id);
        if (skill == null)
        {
            Debug.LogError("未找到该技能，技能ID为:" + id);
            return;
        }
        if (!CanUse(skill)) return;
        StartCoroutine(CoolDown(skill));
        status.SP -= skill.costSP;
        ani.SetBool(skill.animationName, true);
        StartCoroutine(DelayExecute(skill, pos));
    }
    private IEnumerator DelayExecute(SkillData data, Vector3 pos)
    {
        yield return new WaitForSeconds(data.readyTime);
        GameObject prefab;
        switch (data.posType)
        {
            case PosType.Owner:
                prefab = ObjectPool.Instance.GetGameObject(data.name, data.skillPrefab);
                prefab.transform.position = transform.position;
                break;
            case PosType.Dir:
                transform.LookAt(pos);
                prefab = ObjectPool.Instance.GetGameObject(data.name, data.skillPrefab);
                prefab.transform.position = transform.position + Vector3.up;
                break;
            case PosType.Pos:
                transform.LookAt(pos);
                prefab = ObjectPool.Instance.GetGameObject(data.name, data.skillPrefab);
                prefab.transform.position = pos;
                break;
            default:
                prefab = null;
                break;
        }
        prefab.transform.localScale = new Vector3(data.attackDistance, data.attackDistance, data.attackDistance);
        ObjectPool.Instance.DelayRecycle(data.name, prefab, data.durationTime);
        var deployer = prefab.GetComponent<SkillDeployer>();
        deployer.SkillData = data;
        deployer.DeploySkill();
    }
    private IEnumerator CoolDown(SkillData data)
    {
        EventCenter.Instance.EventTrigger<int, int>("CoolDown", data.ID, data.coolTime);
        data.isCooling = true;
        yield return new WaitForSeconds(data.coolTime);
        data.isCooling = false;
    }
    private bool CanUse(SkillData data)
    {
        foreach (var parameter in ani.parameters)//判断执行技能时角色是否在释放其他技能
        {
            if (ani.GetBool(parameter.name))
            {
                return false;
            }
        }
        if (status.SP < data.costSP || data.isCooling)
        {
            return false;
        }
        return true;
    }
}

