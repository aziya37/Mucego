using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour
{
    public static MyLevelManager Instance { get; private set; }

    // SINGLETON
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void GoToNextLevel(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
