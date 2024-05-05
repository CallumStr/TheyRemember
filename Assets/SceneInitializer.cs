using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour
{
    // This method is called when the script instance is being loaded.
    private void Start()
    {
        // Listen for the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Initialize the hotbar slots in the current scene
        InitializeHotbarSlots();
    }

    // This method is called when a scene is loaded.
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reinitialize the hotbar slots when a new scene is loaded
        InitializeHotbarSlots();
    }

    // Initialize the hotbar slots in the current scene
    private void InitializeHotbarSlots()
    {
        // Find all HotbarSlot objects in the scene
        HotbarSlot[] hotbarSlots = FindObjectsOfType<HotbarSlot>();

        // Loop through each HotbarSlot and update its associated item
        foreach (HotbarSlot slot in hotbarSlots)
        {
            // Update the slot with the associated item
            slot.SetItem(slot.associatedItem);
        }
    }
}
