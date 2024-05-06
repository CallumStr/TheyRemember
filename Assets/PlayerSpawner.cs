using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab1; // First player prefab
    public GameObject playerPrefab2; // Second player prefab
    public Transform spawnPoint;     // Spawn point for the player
    public int selectedPlayerIndex = 0; // Index to select which player to spawn

    private bool hasSpawned = false; // Flag to control spawning

    void Start()
    {
        SpawnPlayer(); // Call this function to spawn the player when the game starts
    }

    void SpawnPlayer()
    {
        if (hasSpawned) return; // If the player has already been spawned, do nothing

        GameObject playerToSpawn = selectedPlayerIndex == 0 ? playerPrefab1 : playerPrefab2;

        if (playerToSpawn != null && spawnPoint != null)
        {
            Instantiate(playerToSpawn, spawnPoint.position, spawnPoint.rotation);
            hasSpawned = true; // Set the flag to true to prevent further spawning
        }
        else
        {
            Debug.LogError("PlayerSpawner: Missing player prefab or spawn point.");
        }
    }
}
