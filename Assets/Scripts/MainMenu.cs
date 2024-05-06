using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
   public void ClearInventory()
{
        if (Inventory.instance != null)
        {
            Debug.Log("Found Inventory instance, clearing...");
            Inventory.instance.ClearInventory();
        }
        else
        {
            Debug.LogError("Inventory instance is null");
    }
} // Function to handle the click event of the play button
    public void PlayGame()
    {
        LoadNextScene();
    }

    // Function to start a new game
    public void NewGame()
    {
        ClearInventory();
        Debug.Log("Inventory should be cleared now");
        Invoke("LoadStartingScene", 1); // Delay the scene load by 1 second
    }

    // Function to clear the inventory
    

    // Function to load the starting scene of the game
    private void LoadStartingScene()
    {
        // Assume the starting scene has a build index of 0 or a specific scene name
        SceneManager.LoadScene("CharacterSelection"); // Replace "GameScene" with the actual starting scene name
    }

    // Function to load the next scene in the build order
    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

