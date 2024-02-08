using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject King;
    public GameObject Queen;
    public Transform spawnPoint;
    public static GameObject selectedCharacter;

    public void SelectCharacter(int characterIndex)
    {
        Debug.Log("Selecting character: " + characterIndex);

        // Destroy the previous character if it exists
        if (selectedCharacter != null)
            Destroy(selectedCharacter);

        // Instantiate the chosen character at the specified spawn point
        switch (characterIndex)
        {
            case 0:
                if (King != null)
                {
                    Debug.Log("Instantiating King at position: " + spawnPoint.position);
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
                    Debug.Log("Instantiating Queen at position: " + spawnPoint.position);
                    selectedCharacter = Instantiate(Queen, spawnPoint.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Queen prefab is not assigned.");
                }
                break;
            default:
                Debug.LogError("Invalid character index");
                break;
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("InTheHalls");
    }
}
