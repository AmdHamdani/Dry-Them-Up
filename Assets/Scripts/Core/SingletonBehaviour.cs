using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object padlock = new object();
    public static T Instance
    {
        get
        {
            lock (padlock)
            {
                if (_instance == null)
                {
                    T t = FindObjectOfType<T>();
                    if (t != null)
                    {
                        _instance = t;
                    }
                    else
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        GameObject.DontDestroyOnLoad(singleton);
                    }
                }
                return _instance;
            }

        }
    }
}
