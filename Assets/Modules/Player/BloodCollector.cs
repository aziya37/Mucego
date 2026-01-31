using System.Collections.Generic;
using UnityEngine;

public class BloodCollector : MonoBehaviour
{
    public static BloodCollector Instance { get; private set; }
    public List<PlayerProfile> profiles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SwitchPlayerProfile(int key)
    {
        PlayerController.Instance.currentProfile = profiles[key];
    }
}
