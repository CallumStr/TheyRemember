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
        if (tooltipText != null)
            tooltipText.gameObject.SetActive(false); // Ensure tooltip is initially hidden

        DontDestroyOnLoad(gameObject); // Make the HotbarSlot object persistent

        SetInitialItem(); // Call the method to set the initial item
    }

    private void SetInitialItem()
    {
        // Check if the associated item exists in the player's inventory
        if (playerInventory != null && playerInventory.HasItem(associatedItem))
        {
            SetItem(associatedItem); // Set the initial item if it exists in the inventory
        }
        else
        {
            icon.enabled = false; // Hide the icon if the item is not in the inventory
        }
    }

    public void SetItem(KeyItem item)
    {
        associatedItem = item;
        if (item != null && item.icon != null)
        {
            icon.sprite = item.icon; // Set the item's sprite
            icon.enabled = true; // Make the icon visible
        }
        else
        {
            icon.enabled = false; // Hide the icon if the item is null or lacks an icon
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (associatedItem != null && tooltipText != null && Inventory.instance != null)
        {
            if (Inventory.instance.HasItem(associatedItem))
            {
                // Correct reference to 'description' instead of 'itemDescription'
                tooltipText.text = $"{associatedItem.itemName}\n{associatedItem.description}";
                tooltipText.gameObject.SetActive(true); // Show the tooltip on hover
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipText != null)
            tooltipText.gameObject.SetActive(false); // Hide the tooltip when the pointer exits
    }
}
