using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instence;

    public static T Instence
    {
        get
        {
            if (instence == null)
            {
                instence = FindObjectOfType<T>();
                if(instence == null)
                {
                    var singletonObj = new GameObject();
                    instence = singletonObj.AddComponent<T>();
                    singletonObj.name = typeof(T).ToString() + "(Singleton)";
                    //DontDestroyOnLoad(singletonObj);
                }
            }
            return instence;
        }
    }
}
