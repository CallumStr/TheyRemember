using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    
    // Dialogue arrays for each NPC
    private Dictionary<string, string[]> npcDialogues = new Dictionary<string, string[]>();

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;

    // Reference to the current NPC
    private static NPC currentNPC;

    // Dictionary to store the dialogue index for each NPC instance
    private Dictionary<string, int> dialogueIndices = new Dictionary<string, int>();

    void Start()
    {
        // Assign dialogues for each NPC
        AssignNPCDialogues();

        // Set initial dialogue index for this NPC instance
        dialogueIndices[gameObject.name] = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                currentNPC = this;
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == npcDialogues[gameObject.name][dialogueIndices[gameObject.name]])
        {
            contButton.SetActive(true);
        }
    }

    void AssignNPCDialogues()
    {
        // Assign dialogues for each NPC by name
        if (gameObject.name == "NPC1")
        {
            npcDialogues["NPC1"] = new string[] 
            {
                "First dialogue line for NPC 1",
                "pog",
                // Add more lines as needed
            };
        }
        else if (gameObject.name == "NPC2")
        {
            npcDialogues["NPC2"] = new string[] 
            {
                "First dialogue line for NPC 2",
                "Second dialogue line for NPC 2",
                // Add more lines as needed
            };
        }
        // Add more NPCs as needed
    }

    public void zeroText()
    {
        dialogueText.text = "";
        dialogueIndices[gameObject.name] = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        int index = dialogueIndices[gameObject.name];
        foreach (char letter in npcDialogues[gameObject.name][index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        int index = dialogueIndices[gameObject.name];
        if (index < npcDialogues[gameObject.name].Length - 1)
        {
            dialogueIndices[gameObject.name]++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }

    // Method to continue the dialogue when the continue button is pressed
    public static void ContinueDialogue()
    {
        if (currentNPC != null)
        {
            currentNPC.NextLine();
        }
    }
}