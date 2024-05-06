using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // When the player enters the spawn point, move the player's GameObject to this position
            other.transform.position = transform.position;
        }
    }
}
