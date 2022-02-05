using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Important for SceneManager

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider nextLevel)
    {
        if (nextLevel.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Build index is the Build settings scenes
            // +1 indicates that the index is changing
        }
    }
}
