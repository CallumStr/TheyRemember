using UnityEngine;

public class KeyItemPickup : MonoBehaviour
{
    public KeyItem keyItem; // Reference to the key item associated with this pickup
    public bool canPickup = false; // Flag to indicate if the player can pick up the key item

    private Inventory playerInventory; // Reference to the player's inventory

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true; // Set canPickup to true when a player enters the trigger
            playerInventory = other.GetComponent<Inventory>(); // Cache the player's Inventory component
            Debug.Log("Player entered trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false; // Set canPickup to false when a player exits the trigger
            playerInventory = null; // Clear the cached Inventory component
            Debug.Log("Player exited trigger zone.");
        }
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {   
            Debug.Log("E key pressed.");
            if (playerInventory != null && keyItem != null)
            {
                playerInventory.AddKeyItem(keyItem); // Add the key item to the player's inventory
                gameObject.SetActive(false); // Deactivate the key item GameObject after picking it up

                // Update hotbar display after picking up the key item
                HotbarSlot[] hotbarSlots = FindObjectsOfType<HotbarSlot>();
                foreach (HotbarSlot slot in hotbarSlots)
                {
                    slot.UpdateItemDisplay();
                }

                Debug.Log("Key item picked up.");
                return; // Exit the loop after the item is picked up by one player
            }
            else
            {
                Debug.LogWarning("Player inventory or associated item is null. Cannot pick up item.");
            }
        }
    }
}
