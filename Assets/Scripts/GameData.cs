using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int StartingQuestion;

    void Awake()
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