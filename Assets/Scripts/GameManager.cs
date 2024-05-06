using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject King;
    public GameObject Queen;
    public Transform spawnPoint;

    // Reference to the Inventory script
    private static Inventory inventoryInstance;

    void Start()
    {
        // Spawn the player first
        SpawnSelectedCharacter();

        // Initialize or retrieve the Inventory instance
        InitializeInventory();
    }

    void SpawnSelectedCharacter()
    {
        GameObject selectedCharacter = null;

        switch (CharacterSelection.selectedCharacterIndex)
        {
            case 0:
                if (King != null)
                {
                    selectedCharacter = Instantiate(King, spawnPoint.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("King prefab is not assigned.");
                }
                break;
            case 1:
                if (Queen != null)
                {
                    selectedCharacter = Instantiate(Queen, spawnPoint.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Queen prefab is not assigned.");
                }
                break;
            default:
                Debug.LogError("Invalid character index or character not selected.");
                break;
        }

        if (selectedCharacter != null)
        {
            Debug.Log("Character spawned successfully.");
        }
    }

    void InitializeInventory()
    {
        // Check if the Inventory instance already exists
        if (inventoryInstance == null)
        {
            // Try to find an existing Inventory instance in the scene
            inventoryInstance = FindObjectOfType<Inventory>();

            // If no existing instance is found, create a new one
            if (inventoryInstance == null)
            {
                GameObject inventoryObject = new GameObject("Inventory");
                inventoryInstance = inventoryObject.AddComponent<Inventory>();
                DontDestroyOnLoad(inventoryObject); // Ensure Inventory persists between scenes
            }
        }
        else
        {
            // If the Inventory instance already exists, ensure it's not destroyed on scene load
            DontDestroyOnLoad(inventoryInstance.gameObject);
        }
    }

    // Get the reference to the Inventory instance
    public static Inventory GetInventoryInstance()
    {
        return inventoryInstance;
    }

    // Example method to use the Inventory
    void ExampleMethod()
    {
        Inventory inventory = GetInventoryInstance();
        if (inventory != null)
        {
            // Example usage of Inventory methods
            inventory.AddKeyItem(new KeyItem());
        }
        else
        {
            Debug.LogError("Inventory is not initialized.");
        }
    }
}
