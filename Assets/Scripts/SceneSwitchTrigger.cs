using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchTrigger : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // Or use [SerializeField] private int targetSceneIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Optional: Check if the entering object has a specific tag
        // if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(targetSceneName);
            Debug.Log("Entered");
            // Or use: SceneManager.LoadScene(targetSceneIndex);
        }
    }
}
