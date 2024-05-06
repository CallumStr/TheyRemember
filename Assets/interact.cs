using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public string targetSceneName = "HallwayScene";  // Name of the scene to load

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure the player has a tag "Player"
        {
            Debug.Log("Player is near the door. Press 'E' to enter.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Entering the hallway...");
            SceneManager.LoadScene(targetSceneName);  // Load the hallway scene
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has left the door area.");
        }
    }
}
