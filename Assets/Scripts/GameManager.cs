using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject King;
    public GameObject Queen;
    public Transform spawnPoint;

    void Start()
    {
        SpawnSelectedCharacter();
    }

    void SpawnSelectedCharacter()
    {
        GameObject selectedCharacter = null;

        // Check the selected character index and instantiate the character
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
            // Additional setup for the instantiated character can go here
            Debug.Log("Character spawned successfully.");
        }
    }
}
