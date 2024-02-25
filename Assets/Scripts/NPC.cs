using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;

    private Coroutine typingCoroutine; // creates reference for coroutine so that we can manage its usage better

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
                AssignNPCDialogues();//updates the class so the index value is different each time the player interacts with the NPC
                // StartCoroutine(Typing());
                typingCoroutine = StartCoroutine(Typing()); // resets the coroutine and stores reference
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
            int randomIndex = UnityEngine.Random.Range(0, 2);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["NPC1"] = new string[]
                {
                    "Glory to the Crown!",
                };
            }
            else
            {
                npcDialogues["NPC1"] = new string[]
                {
                    "None may stand against Rúinhaven!",
                };
            }
        }

        if (gameObject.name == "NPC2")
        {
            int randomIndex = UnityEngine.Random.Range(0, 2);
            if (randomIndex == 0)
            {
                npcDialogues["NPC2"] = new string[]
                {
                    "God be with you Your Majesty",
                };
            }
            else //if (randomIndex == 1) only use if the random index range goes above 2
            {
                npcDialogues["NPC2"] = new string[]
                {
                    "Your Highness, I think your consort is looking for you",
                };
            }
            //else
            //{
               // npcDialogues["NPC2"] = new string[] //the text was scrolling too slowly
               // {
                    //"With all the lands in Rúinhaven secured we may now begin to bring order to a restless people",
               // };
            //}
        }
        if (gameObject.name == "NPC3")
        {
            int randomIndex = UnityEngine.Random.Range(0, 2);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["NPC3"] = new string[]
                {
                    "No wine...",
                    "Guess beer will have to do!",
                };
            }
            else
            {
                npcDialogues["NPC3"] = new string[]
                {
                    "Cheers!",
                };
            }
        }
    }

        public void zeroText()
    {
        dialogueText.text = "";
        dialogueIndices[gameObject.name] = 0;
        dialoguePanel.SetActive(false);
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); //Stops the coroutine from running to avoid overflow
        }
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
            if (typingCoroutine != null)//Stops the coroutine from running to avoid overflow
            {
                StopCoroutine(typingCoroutine);
            }
            typingCoroutine = StartCoroutine(Typing());// resets the coroutine and stores reference
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