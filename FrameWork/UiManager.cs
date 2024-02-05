using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


    public enum ULayer
    {
        Bottom,
        Middle,
        Top,
    }
    public class UiManager : Singleton<UiManager>
    {
        private RectTransform canvas;

        private Transform bottom;
        private Transform middle;
        private Transform top;
        protected override void Init()
        {
            base.Init();
            canvas = ResManager.Instance.Load<GameObject>("UI/Prefab/Canvas").transform as RectTransform;
            Object.DontDestroyOnLoad(canvas.gameObject);
            bottom = canvas.Find("Bottom");
            middle = canvas.Find("Middle");
            top = canvas.Find("Top");
        }
        private Dictionary<string, BasePanel> panels = new Dictionary<string, BasePanel>();

        public void ShowPanel<T>(string name, ULayer layer = ULayer.Bottom, UnityAction<T> callback = null) where T : BasePanel
        {
            if (panels.ContainsKey(name))
            {
                panels[name].ShowSelf();
                callback?.Invoke(panels[name] as T);
            }
            ResManager.Instance.LoadAsync<GameObject>("UI/Prefab/" + name, (o) =>
            {
                switch (layer)
                {
                    case ULayer.Bottom:
                        o.transform.SetParent(bottom);
                        break;
                    case ULayer.Middle:
                        o.transform.SetParent(middle);
                        break;
                    case ULayer.Top:
                        o.transform.SetParent(top);
                        break;
                    default:
                        break;
                }
                o.transform.localPosition = Vector3.zero;
                o.transform.localScale = Vector3.one;
                (o.transform as RectTransform).offsetMax = Vector2.one;
                (o.transform as RectTransform).offsetMin = Vector2.zero;
                var script = o.GetComponent<T>();
                callback?.Invoke(script);
                script.ShowSelf();
                panels.Add(name, script);
            });
        }
        public void HidePanel(string name)
        {
            if(panels.ContainsKey(name))
            {
                panels[name].HideSelf();
                Object.Destroy(panels[name].gameObject);
                panels.Remove(name);
            }
        }
        public T GetPanel<T>(string name)where T:BasePanel
        {
            T panel = null;
            if(panels.ContainsKey(name))
            {
                panel = panels[name] as T;
            }
            return panel;
        }
    }

