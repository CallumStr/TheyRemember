using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotbarSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon; // UI component to display the item's icon
    public Text tooltipText; // UI text component for showing item details on hover
    public KeyItem associatedItem; // The current item associated with this slot
    public Inventory playerInventory; // Reference to the player's inventory

    private void Start()
    {
        // Link directly to the singleton instance of Inventory
        playerInventory = Inventory.instance;

        SetInitialItem();
    }

    public void SetInitialItem()
    {
        // Check if the associated item exists in the player's inventory
        if (playerInventory != null && playerInventory.HasItem(associatedItem))
        {
            // Update the item display if it exists in the inventory
            UpdateItemDisplay();
        }
        else
        {
            Debug.Log("Item not found in inventory or inventory not linked");
            // Hide the icon if the item is not in the inventory
            icon.enabled = false;
        }
    }

    public void UpdateItemDisplay()
    {
        // Ensure that the hotbar checks if the item is in the inventory
        if (playerInventory != null && associatedItem != null)
        {
            if (playerInventory.HasItem(associatedItem))
            {
                // Set the item's sprite
                icon.sprite = associatedItem.icon;
                // Make the icon visible
                icon.enabled = true;
            }
            else
            {
                // Hide the icon if the item is not in the inventory
                icon.enabled = false;
            }
        }
        else
        {
            // Hide the icon if no associated item or inventory is found
            icon.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (associatedItem != null && tooltipText != null && playerInventory != null)
        {
            if (playerInventory.HasItem(associatedItem))
            {
                // Show the tooltip on hover
                tooltipText.text = $"{associatedItem.itemName}\n{associatedItem.description}";
                tooltipText.gameObject.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the tooltip when the pointer exits
        if (tooltipText != null)
            tooltipText.gameObject.SetActive(false);
    }
}
