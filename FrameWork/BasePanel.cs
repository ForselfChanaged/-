using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class BasePanel : MonoBehaviour
{
    protected Dictionary<string, List<UIBehaviour>> uiControl = new Dictionary<string, List<UIBehaviour>>();
    private void Awake()
    {

        SearchConponents<Image>();
        SearchConponents<Text>();
        SearchConponents<TextMeshProUGUI>();
        SearchConponents<Button>();
        SearchConponents<Toggle>();
        SearchConponents<Slider>();
        SearchConponents<ScrollRect>();
        SearchConponents<GridLayoutGroup>();
        SearchConponents<RawImage>();
    }
    public T GetControl<T>(string name) where T : UIBehaviour
    {
        T component = null;
        if (uiControl.ContainsKey(name))
        {
            component = uiControl[name].Find(c => c is T) as T;
        }
        return component;
    }
    private void SearchConponents<T>() where T : UIBehaviour
    {
        T[] conponents = this.GetComponentsInChildren<T>();
        string fatherName;
        for (int i = 0; i < conponents.Length; i++)
        {
            fatherName = conponents[i].gameObject.name;
            if (uiControl.ContainsKey(fatherName))
            {
                uiControl[fatherName].Add(conponents[i]);
            }
            else
            {
                uiControl.Add(fatherName, new List<UIBehaviour> { conponents[i] });
            }
            if (conponents[i] is Button)
            {
                (conponents[i] as Button).onClick.AddListener(() => OnClick(fatherName));

            }
            else if (conponents[i] is Toggle)
            {
                (conponents[i] as Toggle).onValueChanged.AddListener((b) => OnValueChange(fatherName, b));
            }
        }
    }

    protected virtual void OnValueChange(string fatherName, bool b)
    {
    }

    protected virtual void OnClick(string fatherName)
    {
    }
    public virtual void ShowSelf()
    {

    }
    public virtual void HideSelf()
    {

    }
}

