using UnityEngine;

/// <summary>
/// Monobehavior 를 싱글턴으로 만들어주는 Generic 클래스
/// </summary>
/// <typeparam name="T"></typeparam>
namespace TIMEnt.Unity
{
    public class TIMSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Get
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj;
                    obj = GameObject.Find(typeof(T).Name);
                    if (obj == null)
                    {
                        obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                    }
                    else
                    {
                        instance = obj.GetComponent<T>();
                    }
                }
                return instance;
            }
        }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}