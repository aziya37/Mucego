using System.Collections;
using UnityEngine;

public class DieAreas : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            StartCoroutine(DieRoutine());
    }

    private void Die()
    {
        DieManager.Instance.ReloadLevel();
    }

    // Corrotinas - Corotines
    private IEnumerator DieRoutine()
    {
        DieManager.Instance.Die();
        yield return new WaitForSeconds(1.2f);
        Die();
    }
}

