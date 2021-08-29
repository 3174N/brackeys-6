using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool Level1 = false, Level2 = false, Level3 = false;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Load()
    {
        if (Level1 && Level2 && Level3)
            FindObjectOfType<LevelLoader>().Load(4);
        else
            FindObjectOfType<LevelLoader>().Load(0);
    }
}
