using UnityEngine;

[CreateAssetMenu(fileName = "skillData", menuName = "CreateSkill")]
public class SkillData : ScriptableObject
{
    public int ID;
    public string Name;
    [TextArea]
    public string description;
    public bool isCooling;
    public PosType posType;
    public float readyTime;
    public int coolTime;
    public int costSP;
    public float maxDistance;
    public float attackDistance;
    [Range(45, 360)]
    public float attackAngle;
    public string[] targetTags;
    public Transform[] targets;
    public ImpactType[] impactType;
    public float atkRatio;
    public float durationTime;
    public float atkInterval;
    public GameObject owner;
    public GameObject skillPrefab;
    public string animationName;
    public Sprite skillSprite;
    public GameObject hitFixPerfab;
    public int level;
    public AttackType attackType;
    public SelectorType seletorType;
    public AreaType skillAreaType;
}
public enum PosType
{
    Owner,
    Dir,
    Pos,
}

