using System.Collections;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    
    public bool isLevelCompleted = false;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ball"))
        {
            isLevelCompleted = true;
            Debug.Log("Level Completed!");
            StartCoroutine(DelayedDestroy());
            
        }
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.FindWithTag("Ball").SetActive(false);
    }
}
