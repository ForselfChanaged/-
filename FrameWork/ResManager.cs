using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class ResManager : Singleton<ResManager>
{
    public T Load<T>(string path) where T : Object
    {
        var res = Resources.Load<T>(path);
        if (res == null)
        {
            Debug.LogError("未找到资源，错误路径：" + path);
            return null;
        }
        if (res is GameObject)
        {
            return GameObject.Instantiate(res);
        }
        return res;
    }
    public void LoadAsync<T>(string path, UnityAction<T> callback) where T : Object
    {
        MonoManager.Instance.StartCoroutine(ReallyLoad(path, callback));
    }
    private IEnumerator ReallyLoad<T>(string path, UnityAction<T> callback) where T : Object
    {
        var request = Resources.LoadAsync<T>(path);
        if (request == null)
            Debug.LogError("未找到资源，错误路径：" + path);
        yield return request;
        if (request.asset is GameObject)
        {
            callback?.Invoke(GameObject.Instantiate(request.asset as T));
        }
        else
        {
            callback?.Invoke(request.asset as T);
        }
    }
}

