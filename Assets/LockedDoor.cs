using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LockedDoor : MonoBehaviour
{
    public KeyItem keyItem1;
    public KeyItem keyItem2;
    public KeyItem keyItem3;
    public GameObject messagePanel;
    public Text messageText;

    private Inventory globalInventory;
    private bool isOpenable = false;

    void Start()
    {
        // Assuming the Inventory component is a singleton or is globally accessible
        globalInventory = Inventory.instance;
        messagePanel.SetActive(false); // Hide the message panel initially
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpenable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOpenable = false;
            HideMessage(); // Hide the message panel when the player moves away from the door
        }
    }

    void Update()
    {
        if (isOpenable && Input.GetKeyDown(KeyCode.E))
        {
            AttemptToOpenDoor();
        }
    }

    void AttemptToOpenDoor()
    {
        // Check if the global inventory has all three key items
        if (globalInventory != null &&
            globalInventory.HasItem(keyItem1) &&
            globalInventory.HasItem(keyItem2) &&
            globalInventory.HasItem(keyItem3))
        {
            // Open the door (trigger opening animation, deactivate collider, etc.)
            Debug.Log("Door opened!");
            SceneManager.LoadScene("Secret room");
        }
        else
        {
            // Player does not have all required key items
            Debug.Log("You need all three key items to open this door.");
            ShowMessage("You must find all three key items to open this door.");
        }
    }

    void ShowMessage(string text)
    {
        messageText.text = text;
        messagePanel.SetActive(true); // Show the message panel
    }

    void HideMessage()
    {
        messagePanel.SetActive(false); // Hide the message panel
    }
}
