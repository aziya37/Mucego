using UnityEngine;

public class BloodCollectable : MonoBehaviour
{
    public enum BloodType{
        SLOW = 0,
        NORMAL = 1,
        FAST = 2
    };
    
    public BloodType bloodType;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
            Collect();
    }

    private void Collect()
    {
        BloodCollector.Instance.SwitchPlayerProfile((int)bloodType);
        transform.gameObject.SetActive(false);
    }
}
