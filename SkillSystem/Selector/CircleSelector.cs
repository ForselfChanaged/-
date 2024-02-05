using System.Collections.Generic;
using System.Linq;
using UnityEngine;
class CircleSelector : ISelector
{
    public Transform[] SelectorTarget(SkillData data, Transform skTF)
    {

        List<Transform> targets = new List<Transform>();
        //选择出所有目标类型对象
        for (int i = 0; i < data.targetTags.Length; i++)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(data.targetTags[i]);
            targets.AddRange(gameObjects.Select(g => g.GetComponent<Transform>()));
        }
        //区域内选择
        targets = targets.FindAll(t =>
                          (Vector3.Distance(skTF.position, t.position) < data.attackDistance
                         && Vector3.Angle(skTF.forward, t.position - skTF.position) < data.attackAngle / 2)
                         );
        targets = targets.FindAll(t => t.GetComponent<CharacterStatus>().HP > 0);

        if (data.attackType == AttackType.multiply || targets.Count == 0)
        {
            Transform[] result = targets.ToArray();
            return result;
        }
        Transform target = targets.GetMin(t => Vector3.Distance(t.position, skTF.position));
        return new Transform[] { target };
    }

}
