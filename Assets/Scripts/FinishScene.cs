using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Important for SceneManager

public class FinishScene : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider finalLevel)
    {
        if (finalLevel.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); // Build index is the Build settings scenes
            // +1 indicates that the index is changing
        }
    }
}
