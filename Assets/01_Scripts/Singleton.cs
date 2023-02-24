using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected abstract void Awake();

    private static T instance;
    public static T Instance
    {
        get { 
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
            }
            return instance; 
        }
        set
        {
            if(instance != null)
            {
                Debug.Log("An instance of this singleton already exists: " + typeof(T).Name);
                Destroy(instance);
            }
            instance = value;
        }
    }
}