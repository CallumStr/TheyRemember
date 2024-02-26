using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine; // Make sure to include the Cinemachine namespace

public class CharacterSelection : MonoBehaviour
{
    public static int selectedCharacterIndex = -1; 
    public GameObject King;
    public GameObject Queen;
    public Transform spawnPoint;
    public static GameObject selectedCharacter;

    public void SelectCharacter(int characterIndex)
    {
        Debug.Log("Selecting character: " + characterIndex);
        selectedCharacterIndex = characterIndex;

        if (selectedCharacter != null)
            Destroy(selectedCharacter);

        GameObject prefabToInstantiate = null;
        switch (characterIndex)
        {
            case 0:
                prefabToInstantiate = King;
                break;
            case 1:
                prefabToInstantiate = Queen;
                break;
            default:
                Debug.LogError("Invalid character index");
                return;
        }

        if (prefabToInstantiate != null)
        {
            selectedCharacter = Instantiate(prefabToInstantiate, spawnPoint.position, Quaternion.identity);
            // After instantiation, try to find the Cinemachine Virtual Camera in the instantiated character
            CinemachineVirtualCamera characterCamera = selectedCharacter.GetComponentInChildren<CinemachineVirtualCamera>();
            if (characterCamera != null)
            {
                // If the camera is found, activate it or ensure it's set up correctly
                characterCamera.gameObject.SetActive(true); // Make sure the camera is active
                characterCamera.Follow = selectedCharacter.transform;
                characterCamera.LookAt = selectedCharacter.transform;

                // Adjust the virtual camera's priority if necessary
                characterCamera.Priority = 10;
            }
            else
            {
                Debug.LogError("No Cinemachine Virtual Camera found on the selected character prefab.");
            }
        }
        else
        {
            Debug.LogError($"{(characterIndex == 0 ? "King" : "Queen")} prefab is not assigned.");
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("InTheHalls");
    }
}
