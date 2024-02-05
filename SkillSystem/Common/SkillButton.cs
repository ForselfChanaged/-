using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    private Transform delt;
    private Image bg;
    private Image coolDownImage;
    private Vector2 thisWorldPos = Vector2.zero;
    private Vector2 vDelt;
    public int id;
    public float touchRadius;
    public Vector3 targetPos;

    public Action<int,Vector3> UpHandler;
    public Action<Vector2> DragHandler;
    public Action<Vector2> DownHandler;
    private void Awake()
    {
        delt = transform.FindInChild("Delt");
        bg = transform.FindInChild("Bg").GetComponent<Image>();
        coolDownImage = transform.FindInChild("CoolDownImage").GetComponent<Image>();
        thisWorldPos = transform.position;
    }
    public void Init(Sprite sprite,int _id)
    {
        id = _id;
        bg.sprite = sprite;
        coolDownImage.sprite = sprite;
        EventCenter.Instance.AddListener<int, int>("CoolDown", CallBack);
    }

    private void CallBack(int _id, int time)
    {
      if(_id==id)
        {
            CoolDown(time);
        }
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<int, int>("CoolDown", CallBack);
    }
    private void CoolDown(float time)
    {
        StartCoroutine(BeginCoolDown(time));
    }
    IEnumerator BeginCoolDown(float time)
    {
        coolDownImage.fillAmount = 0;
        float cTime = 0;
        while (cTime < time)
        {
            cTime += Time.deltaTime;
            coolDownImage.fillAmount = (cTime / time);
            yield return null;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        delt.localPosition = Vector3.zero;
        EventCenter.Instance.EventTrigger<int>("SkillButtonUp", id);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 relativePos = eventData.position - thisWorldPos;
       if(relativePos.magnitude<touchRadius)
        {
            delt.position = eventData.position;
        }
       else
        {
            delt.position = relativePos.normalized * touchRadius + thisWorldPos;
        }
        vDelt = delt.localPosition / touchRadius;
        if (vDelt.magnitude > 1) vDelt = vDelt.normalized;
        EventCenter.Instance.EventTrigger<int, Vector3>("SkillButtonDrag", id, vDelt);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        thisWorldPos = transform.position;
        delt.position = eventData.position;
        vDelt = delt.localPosition / touchRadius;
        EventCenter.Instance.EventTrigger<int, Vector3>("SkillButtonDown", id, vDelt);
    }

}


