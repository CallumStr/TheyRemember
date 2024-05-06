using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotbarSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon; // UI component to display the item's icon
    public Text tooltipText; // UI text component for showing item details on hover
    public KeyItem associatedItem; // The current item associated with this slot

    private void Start()
    {
        // Set the initial item display
        UpdateItemDisplay();
    }

    // Update the item display based on inventory changes
    public void UpdateItemDisplay()
    {
        // Check if the associated item is in the player's inventory
        if (Inventory.instance != null && associatedItem != null && Inventory.instance.HasItem(associatedItem))
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (associatedItem != null && tooltipText != null && Inventory.instance != null && Inventory.instance.HasItem(associatedItem))
        {
            // Show the tooltip on hover only if the item is in the player's inventory
            tooltipText.text = $"{associatedItem.itemName}\n{associatedItem.description}";
            tooltipText.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the tooltip when the pointer exits
        if (tooltipText != null)
            tooltipText.gameObject.SetActive(false);
    }
}
