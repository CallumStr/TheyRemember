using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Start()
    {
        // Find the player object by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Move the player to the spawn point position
            player.transform.position = transform.position;
        }
        else
        {
            Debug.LogWarning("Player object not found. Make sure the player object has the 'Player' tag.");
        }
    }
}
