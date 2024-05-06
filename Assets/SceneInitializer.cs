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
        HotbarSlot[] hotbarSlots = FindObjectsOfType<HotbarSlot>();

        foreach (HotbarSlot slot in hotbarSlots)
        {
            slot.playerInventory = Inventory.instance;  // Ensure each slot links to the persistent inventory
            slot.SetInitialItem();
        }
    }

}
