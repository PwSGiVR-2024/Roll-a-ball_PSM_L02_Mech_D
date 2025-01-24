using UnityEngine;

public class MusicObjectSingletonScript : MonoBehaviour
{
    public static MusicObjectSingletonScript Instance;
    public bool ForceNewInstance = false;

    private void Awake()
    {
        if (ForceNewInstance)
        {
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}