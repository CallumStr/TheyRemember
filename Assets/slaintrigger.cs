using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SlainTrigger : MonoBehaviour
{
    public float delayBeforeReturn = 3f; // Delay before returning to the main menu scene
    public Image blackoutImage; // Reference to the image used for screen blackout
    public Text slainText; // Reference to the text displaying "You were slain for your sins"
    public Canvas canvasToEnable; // Reference to the canvas to enable

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SlainSequence());
        }
    }

    IEnumerator SlainSequence()
    {
        // Set the blackout image to fully opaque
        blackoutImage.color = Color.black;
        blackoutImage.gameObject.SetActive(true);

        // Display the "You were slain for your sins" text
        slainText.gameObject.SetActive(true);

        // Enable the canvas
        canvasToEnable.gameObject.SetActive(true);

        // Wait for a delay before returning to the main menu
        yield return new WaitForSeconds(delayBeforeReturn);

        // Return to the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
