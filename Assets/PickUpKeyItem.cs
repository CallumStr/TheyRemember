using UnityEngine;

public class KeyItemPickup : MonoBehaviour
{
    public KeyItem keyItem; // Reference to the key item Scriptable Object
    public HotbarSlot hotbarSlot; // Reference to the HotbarSlot component associated with this key item

    private bool canPickup = false; // Flag to indicate if the player can pick up the item
    private GameObject player; // Reference to the player GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            canPickup = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player exits the trigger zone
        {
            canPickup = false;
            player = null;
        }
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            Inventory playerInventory = player.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                playerInventory.AddKeyItem(keyItem); // Add the key item to the player's inventory

                if (hotbarSlot != null && keyItem != null && keyItem.icon != null)
                {
                    hotbarSlot.SetItem(keyItem); // Set the key item to the hotbar slot
                }
                else
                {
                    Debug.LogWarning("Hotbar slot or key item or icon is not properly assigned.");
                }

                gameObject.SetActive(false); // Deactivate the key item GameObject after picking it up
            }
        }
    }
}
