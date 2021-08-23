using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using TMPro;

public class LevelLoader : MonoBehaviour
{
    // To use from another script use:
    // FindObjectOfType<LevelLoader>()

    public float TransitionTime = 1f;
    private Animator _transition;

    private void Awake()
    {
        _transition = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// Loads a specific level.
    /// </summary>
    /// <param name="levelIndex">Level index.</param>
    public void Load(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    /// <summary>
    /// Loads a specific level.
    /// </summary>
    /// <param name="levelIndex">Level name.</param>
    public void Load(string levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    /// <summary>
    /// Loads next level.
    /// </summary>
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    /// <summary>
    /// Loads the first level.
    /// </summary>
    public void LoadStart()
    {
        StartCoroutine(LoadLevel(1));
    }

    /// <summary>
    /// Loads the menu level.
    /// </summary>
    public void LoadMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadCurrentLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        _transition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
    }

    IEnumerator LoadLevel(string levelIndex)
    {
        _transition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
    }
}