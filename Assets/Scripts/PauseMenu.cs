using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuObject;

    public KeyCode PauseKey = KeyCode.Escape;

    bool isPaused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PauseKey))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void Pause()
    {
        isPaused = true;
        PauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resumes the game.
    /// </summary>
    public void Resume()
    {
        isPaused = false;
        PauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Restarts the level.
    /// </summary>
    public void Restart()
    {
        Resume();
        FindObjectOfType<LevelLoader>().LoadCurrentLevel();
    }

    /// <summary>
    /// Loads menu level.
    /// </summary>
    public void Menu()
    {
        Resume();
        FindObjectOfType<AudioManager>().Stop("Stress");
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<LevelLoader>().LoadMenu();
    }
}