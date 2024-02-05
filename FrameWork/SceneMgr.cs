using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


    public class SceneMgr : Singleton<SceneMgr>
    {
        public void LoadScene(string sceneName,UnityAction action)
        {
            SceneManager.LoadScene(sceneName);
            action?.Invoke();
        }
        public void LoadSceneAsync(string sceneName,UnityAction action = null)
        {
            MonoManager.Instance.StartCoroutine(ReallyLoadScene(sceneName,action));
        }
        private IEnumerator ReallyLoadScene(string sceneName,UnityAction action)
        {
            var ao = SceneManager.LoadSceneAsync(sceneName);
            while (!ao.isDone)
            {
                yield return null;
            }
            action?.Invoke();
        }
    }

