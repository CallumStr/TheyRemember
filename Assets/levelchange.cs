using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
    public float interactionDistance = 3f; // Maximum distance for interaction
    public KeyCode interactionKey = KeyCode.E; // Key to interact with the door

    private Transform player; // Reference to the player's transform

    private void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Ensure that the player object persists across scene changes
        DontDestroyOnLoad(player.gameObject);
    }

    private void Update()
    {
        // Calculate the distance between the player and the door
        float distance = Vector3.Distance(transform.position, player.position);

        // Check if the player is within interaction distance and if they press the interaction key
        if (distance <= interactionDistance && Input.GetKeyDown(interactionKey))
        {
            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Visualize the interaction distance in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
