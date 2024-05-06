using UnityEngine;

[CreateAssetMenu(fileName = "New KeyItem", menuName = "Inventory/KeyItem")]
public class KeyItem : ScriptableObject
{
    [Header("Item Properties")]
    public string itemName;
    public string description;
    public Sprite icon;

    [Header("Key Item Properties")]
    public bool isConsumable; // Whether the item is consumed upon use
    public bool isStoryItem; // Whether the item is a key story item
    public bool isHotbarItem; // Whether the item should appear in the hotbar

    // Additional properties specific to key story items can be added here

    // Method to use the key item (if applicable)
    public virtual void UseItem()
    {
        // Implement functionality for using the key item
        // For example, unlocking a door or triggering a story event
        Debug.Log("Using " + itemName);
    }
}
