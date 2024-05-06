using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene to load

    private bool canInteract = false; // Flag to indicate if the player can interact with the door

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true; // Set canInteract to true when a player enters the trigger zone
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false; // Set canInteract to false when a player exits the trigger zone
        }
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            // Load the next scene when the player presses "E"
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
