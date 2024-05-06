using UnityEngine;

public class KeyItemPickup : MonoBehaviour
{
    public KeyItem keyItem; // Reference to the key item associated with this pickup
    public bool canPickup = false; // Flag to indicate if the player can pick up the key item

    private Inventory globalInventory; // Reference to the global inventory

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true; // Set canPickup to true when a player enters the trigger
            globalInventory = FindObjectOfType<Inventory>(); // Find the global inventory in the scene
            Debug.Log("Player entered trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false; // Set canPickup to false when a player exits the trigger
            globalInventory = null; // Clear the cached global inventory component
            Debug.Log("Player exited trigger zone.");
        }
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {   
            Debug.Log("E key pressed.");
            if (globalInventory != null && keyItem != null)
            {
                globalInventory.AddKeyItem(keyItem); // Add the key item to the global inventory
                gameObject.SetActive(false); // Deactivate the key item GameObject after picking it up

                // Update hotbar display after picking up the key item
                HotbarSlot[] hotbarSlots = FindObjectsOfType<HotbarSlot>();
                foreach (HotbarSlot slot in hotbarSlots)
                {
                    slot.UpdateItemDisplay();
                }

                Debug.Log("Key item picked up and added to global inventory.");
                return; // Exit the loop after the item is picked up by one player
            }
            else
            {
                Debug.LogWarning("Global inventory or associated item is null. Cannot pick up item.");
            }
        }
    }
}
