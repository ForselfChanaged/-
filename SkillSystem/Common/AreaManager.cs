using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 生成技能指示区域，测算技能释放位置
/// </summary>
public class AreaManager : MonoSingleton<AreaManager>
{
    public List<SkillData> skills;
    private SkillData currentSkill;
    private GameObject skillAreaObj;
    private float outRadius;
    private float innerRadius;
    private Vector3 targetPos;
    //提供方法生成技能区域，需要参数技能指示器类型，技能释放范围，施法距离
    //不断更新指示器位置

    private Dictionary<Element, Transform> allElementTrans;
    private Dictionary<Element, string> allElementPath;
    private string rootPath = "SkillIndicator/";
    private string circle = "circle";
    private string rectangle = "rectangle";
    private string sector = "sector";
    protected override void Init()
    {
        base.Init();
        allElementPath = new Dictionary<Element, string>();
        allElementPath.Add(Element.Outcircle, rootPath + circle);
        allElementPath.Add(Element.InnerCircle, rootPath + circle);
        allElementPath.Add(Element.Rectangle, rootPath + rectangle);
        allElementPath.Add(Element.Sector, rootPath + sector);
        allElementTrans = new Dictionary<Element, Transform>();
        allElementTrans.Add(Element.Outcircle, null);
        allElementTrans.Add(Element.InnerCircle, null);
        allElementTrans.Add(Element.Sector, null);
        allElementTrans.Add(Element.Rectangle, null);
    }
    private void OnEnable()
    {
        EventCenter.Instance.AddListener<int, Vector3>("SkillButtonDown", (id, pos) => GeneratorArea(id));
        EventCenter.Instance.AddListener<int, Vector3>("SkillButtonDrag", (id, pos) => UpdateArea(currentSkill.skillAreaType, pos));
        EventCenter.Instance.AddListener<int>("SkillButtonUp", (id) => HideArea());
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<int, Vector3>("SkillButtonDown", (id, pos) => GeneratorArea(id));
        EventCenter.Instance.RemoveListener<int, Vector3>("SkillButtonDrag", (id, pos) => UpdateArea(currentSkill.skillAreaType, pos));
        EventCenter.Instance.RemoveListener<int>("SkillButtonUp", (id) => HideArea());
    }
    private void GeneratorArea(int ID)
    {
        currentSkill = skills.Find(s => s.ID == ID);
        GeneratorArea(currentSkill.skillAreaType, currentSkill.maxDistance, currentSkill.attackDistance);
    }
    private void GeneratorArea(AreaType type, float outR = 1, float inR = 1)
    {
        outRadius = outR;
        innerRadius = inR;
        switch (type)
        {
            case AreaType.Circle:
                GeneratorElement(Element.Outcircle);
                break;
            case AreaType.Sector:
                GeneratorElement(Element.Sector);
                break;
            case AreaType.Rectangle:
                GeneratorElement(Element.Rectangle);
                break;
            case AreaType.Outer_InnerCircle:
                GeneratorElement(Element.Outcircle);
                GeneratorElement(Element.InnerCircle);
                break;
            default:
                break;
        }
    }
    private void UpdateArea(AreaType type, Vector3 delt)
    {
        switch (type)
        {
            case AreaType.Circle:
                break;
            case AreaType.Sector:
                allElementTrans[Element.Rectangle].LookAt(GetDir(delt));
                targetPos = GetDir(delt);
                break;
            case AreaType.Rectangle:
                allElementTrans[Element.Rectangle].LookAt(GetDir(delt));
                targetPos = GetDir(delt);
                break;
            case AreaType.Outer_InnerCircle:
                allElementTrans[Element.InnerCircle].position = GetDir(delt);
                targetPos = GetDir(delt);
                break;
            default:

                break;
        }
    }
    private Vector3 GetDir(Vector3 delt)
    {
        Vector3 v = new Vector3(-delt.x, 0, -delt.y);
        Vector3 pos = skillAreaObj.transform.position + (v * -outRadius);
        return pos;
    }

    private void HideArea()
    {
        EventCenter.Instance.EventTrigger<Vector3>("ThisPos", targetPos);
        for (int i = 0; i < skillAreaObj.transform.childCount; i++)
        {
            skillAreaObj.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void GeneratorElement(Element element)
    {
        Transform eTrans = GetElement(element);
        allElementTrans[element] = eTrans;
        switch (element)
        {
            case Element.Outcircle:
                eTrans.localScale = new Vector3(outRadius * 2, 1, outRadius * 2);
                eTrans.gameObject.SetActive(true);
                break;
            case Element.Rectangle:
                eTrans.localScale = new Vector3(innerRadius, 1, outRadius);
                eTrans.gameObject.SetActive(true);
                break;
            case Element.Sector:
                eTrans.localScale = new Vector3(outRadius, 1, outRadius);
                eTrans.gameObject.SetActive(true);
                break;
            case Element.InnerCircle:
                eTrans.localScale = new Vector3(innerRadius * 2, 1, innerRadius * 2);
                eTrans.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    private Transform GetElement(Element element)
    {
        if (skillAreaObj == null) CreateAreaObj();
        Transform parent = skillAreaObj.transform;
        string name = element.ToString();
        Transform elementTrans = parent.FindInChild(name);
        if (elementTrans == null)
        {
            GameObject o = ResManager.Instance.Load<GameObject>(allElementPath[element]);
            o.transform.SetParent(parent);
            o.name = name;
            o.SetActive(false);
            elementTrans = o.transform;
        }
        elementTrans.localPosition = Vector3.zero;
        elementTrans.localEulerAngles = Vector3.zero;
        elementTrans.localScale = Vector3.one;
        return elementTrans;
    }
    private void CreateAreaObj()
    {
        GameObject player = GameObject.FindWithTag("Player");
        skillAreaObj = new GameObject("SkillArea");
        skillAreaObj.transform.parent = player.transform;
        skillAreaObj.transform.localPosition = Vector3.zero;
    }
}


