using System.Collections.Generic;
using UnityEngine;
using TMPro;  // This namespace is required to use TMP_Text

public class Inventory : MonoBehaviour
{
    private List<KeyItem> keyItems = new List<KeyItem>();
    private List<KeyItem> hotbarItems = new List<KeyItem>(); // List to store items in the hotbar
    public TMP_Text inventoryText;
    public TMP_Text hotbarText;
    public int hotbarSize = 5; // Size of the hotbar

    // Method to add a key item to the inventory
    public void AddKeyItem(KeyItem item)
    {
        if (!keyItems.Contains(item))
        {
            keyItems.Add(item);
            UpdateInventoryText();
        }
    }

    // Method to remove a key item from the inventory
    public void RemoveKeyItem(KeyItem item)
    {
        if (keyItems.Remove(item))
        {
            UpdateInventoryText();
        }
    }

    // Method to check if the inventory contains a specific item
    public bool HasItem(KeyItem item)
    {
        return keyItems.Contains(item);
    }

    private void UpdateInventoryText()
    {
        inventoryText.text = "Inventory:\n";
        foreach (KeyItem item in keyItems)
        {
            inventoryText.text += "- " + item.itemName + "\n";
        }
    }

    private void UpdateHotbarText()
    {
        hotbarText.text = "Hotbar:\n";
        foreach (KeyItem item in hotbarItems)
        {
            hotbarText.text += "- " + item.itemName + "\n";
        }
    }
}
