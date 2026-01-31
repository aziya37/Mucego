using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieManager : MonoBehaviour
{
    public static DieManager Instance { get; private set; }
    public Material fullscreenMaterial;
    
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
    
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Die(){
        StartCoroutine(FullscreenTransition());
    }
    public IEnumerator FullscreenTransition()
    {
        float transitionDuration = 1.0f;
        float currentTime = 0.0f;
        while(currentTime < transitionDuration)
        {
            fullscreenMaterial.SetFloat("_Transition", (currentTime / transitionDuration) * 1.5f);
            currentTime += Time.deltaTime;
            yield return null;
        }
        fullscreenMaterial.SetFloat("_Transition", 0.0f);

    }
}
