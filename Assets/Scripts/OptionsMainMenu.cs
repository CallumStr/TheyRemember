using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButton : MonoBehaviour
{
    public int optionsSceneIndex = 3;  // scene index for options page
    public int defaultSceneIndex = 0;  // default value for scene idex

    private static int previousSceneIndex = -1; // Static to persist across scenes

    //loads options page and stores the last index
    public void NavOptions()
    {
        StorePreviousScene();
        LoadSceneByIndex(optionsSceneIndex);
    }

    // Call this method to navigate back to the previous scene or Main Menu
    public void NavPreviousIndex()
    {
        if (previousSceneIndex >= 0 && previousSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadSceneByIndex(previousSceneIndex); //loads previous index
        }
        else
        {
            LoadSceneByIndex(defaultSceneIndex); // otherwise loads default value
        }
    }

    // stores index value
    private void StorePreviousScene()
    {
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // method for loading scene
    private void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
    //uses the graphics presets and changes it
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
