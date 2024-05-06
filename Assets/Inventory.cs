using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; // Singleton instance

    // Delegate and event for inventory changes
    public delegate void InventoryChanged();
    public event InventoryChanged OnInventoryChanged;

    private List<KeyItem> keyItems = new List<KeyItem>(); // List of collected key items

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // This ensures the inventory persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to add a key item to the inventory
    public void AddKeyItem(KeyItem item)
    {
        if (!keyItems.Contains(item))
        {
            keyItems.Add(item);
            if (OnInventoryChanged != null) OnInventoryChanged.Invoke();
        }
    }

    // Method to check if a key item is in the inventory
    public bool HasItem(KeyItem item)
    {
        return keyItems.Contains(item);
    }

    // Method to remove a key item from the inventory
    public void RemoveKeyItem(KeyItem item)
    {
        keyItems.Remove(item);
        if (OnInventoryChanged != null) OnInventoryChanged.Invoke();
    }

    // Method to clear all items from the inventory
    public void ClearInventory()
    {
        keyItems.Clear();
        if (OnInventoryChanged != null) OnInventoryChanged.Invoke();
    }

    // Method to retrieve the list of key items
    public List<KeyItem> GetKeyItems()
    {
        return keyItems;
    }
}
