using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // Singleton instance

    // Make keyItems accessible by other scripts
    public List<KeyItem> keyItems = new List<KeyItem>(); 

    private List<KeyItem> hotbarItems = new List<KeyItem>(); 
    public TMP_Text inventoryText;
    public TMP_Text hotbarText;
    public int hotbarSize = 5; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the instance to this object if it doesn't exist
            DontDestroyOnLoad(gameObject); // Make the object persist between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void AddKeyItem(KeyItem item)
    {
        if (!keyItems.Contains(item))
        {
            keyItems.Add(item);
            UpdateInventoryText();
        }
    }

    public void RemoveKeyItem(KeyItem item)
    {
        if (keyItems.Remove(item))
        {
            UpdateInventoryText();
        }
    }

    public bool HasItem(KeyItem item)
    {
        return keyItems.Contains(item);
    }

    public void AddToHotbar(KeyItem item)
    {
        if (hotbarItems.Count < hotbarSize && !hotbarItems.Contains(item))
        {
            hotbarItems.Add(item);
            UpdateHotbarText();
        }
    }

    public void RemoveFromHotbar(KeyItem item)
    {
        if (hotbarItems.Remove(item))
        {
            UpdateHotbarText();
        }
    }

    private void UpdateInventoryText()
    {
        if (inventoryText != null) // Check if inventory text is assigned
        {
            inventoryText.text = "Inventory:\n";
            foreach (KeyItem item in keyItems)
            {
                inventoryText.text += "- " + item.itemName + "\n";
            }
        }
    }

    private void UpdateHotbarText()
    {
        if (hotbarText != null) // Check if hotbar text is assigned
        {
            hotbarText.text = "Hotbar:\n";
            foreach (KeyItem item in hotbarItems)
            {
                hotbarText.text += "- " + item.itemName + "\n";
            }
        }
    }
}
