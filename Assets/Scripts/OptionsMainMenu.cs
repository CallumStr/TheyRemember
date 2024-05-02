using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButton : MonoBehaviour
{
    public int optionsSceneIndex = 3;  // scene index for options page
    public int defaultSceneIndex = 0;  // default value for scene idex

    private static int previousSceneIndex = -1; // Static to persist across scenes

    private bool isPaused = false;

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

    public void ResumeInGame()
    {
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    // Start is called before the first frame update
    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public TMP_Dropdown resolutionDropdown; // Reference to the existing dropdown component

    private List<Resolution> filteredResolutions; // List of specific resolutions to display

    void Start()
    {
        // Define the allowed resolutions
        filteredResolutions = new List<Resolution>
        {
            new Resolution { width = 1280, height = 720 },   // 720p
            new Resolution { width = 1920, height = 1080 },  // 1080p
            new Resolution { width = 2560, height = 1440 }   // 1440p
        };

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        bool currentResolutionMatched = false;

        // Populate the dropdown with only allowed resolutions
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            Resolution res = filteredResolutions[i];
            string option = res.width + " x " + res.height;
            options.Add(option);

            // Check if this is the current resolution
            if (!currentResolutionMatched &&
                res.width == Screen.currentResolution.width &&
                res.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
                currentResolutionMatched = true;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        // Set resolution based on filtered list, not the full list
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
