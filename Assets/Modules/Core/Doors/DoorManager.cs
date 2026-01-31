using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public bool OnDoor;
    public int nextSceneID;
    public InputManager inputManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        OnDoor = collision.CompareTag("Player");
       
    }
    void OnTriggerExit2D(Collider2D collision)
    {
       OnDoor = false; 
    }
    void Start()
    {
        inputManager.OnInteract += GoToNextLevel;
    }

    void GoToNextLevel()
    {
        if(OnDoor)
            MyLevelManager.Instance.GoToNextLevel(nextSceneID);
    }
}
