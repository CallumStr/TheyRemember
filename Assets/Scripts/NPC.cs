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

        if (gameObject.name == "NPC4")
        {
            int randomIndex = UnityEngine.Random.Range(0, 1);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["NPC4"] = new string[]
                {
                    "How may I serve your majesty?"
                };
            }
        }

        if (gameObject.name == "NPC5")
        {
            int randomIndex = UnityEngine.Random.Range(0, 1);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["NPC5"] = new string[]
                {
                    "Good tidings m'liege"
                };
            }
        }

        if (gameObject.name == "NPC6")
        {
            int randomIndex = UnityEngine.Random.Range(0, 1);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["NPC6"] = new string[]
                {
                    "Sorry my lord we appear to be out of wine, there is a bottle in the cellar",
                };
            }
        }

        if (gameObject.name == "NPC7")
        {
            int randomIndex = UnityEngine.Random.Range(0, 1);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["NPC7"] = new string[]
                {
                    "Strumming a tune on his lute",
                };
            }
        }

        if (gameObject.name == "Lore1")
        {
            int randomIndex = UnityEngine.Random.Range(0, 1);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["Lore1"] = new string[]
                {
                    "Kings Diary Entry Part One",
                    "Date: 29th February, 1444",
                    "Today marks a historic day for the people of Rúinhaven as we march forward to expand our glorious kingdom, so that the Lord might smile down upon us with glee. The knights and levies are high in spirit, and our holy conquest to expand may be completed without a hitch. As the sovereign, my decree is final and may only be succeeded for the goodwill of our countrymen.",
                    "Date: 15th April, 1444",
                    "The days and months have been hard on our soldiers. We fight a people who do not share our values, and thus they oppose our will. Many have died in this campaign, and I worry for the morale of our forces as acts of violence towards the people we seek to add to our kingdom grow. Despite this, our armies are unmatched against our foes, and towns and hamlets fall under our might. However, I cannot escape this feeling that we are being judged by an unknown force. I feel it, and so do our forces.",
                    "Seems to be another part",
                };
            }
        }

        if (gameObject.name == "Lore2")
        {
            int randomIndex = UnityEngine.Random.Range(0, 1);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["Lore2"] = new string[]
                {
                    "Kings Diary Entry Part Two",
                    "Date: 29th July, 1444",
                    "The gods seem fit to test me, and I now question the piety of our conquest. Our enemies utterly oppose us and refuse to surrender. They seemed to have razed the lands and killed the livestock in an attempt to slow our advances. Many people have died unjustly in this war, and many of our best have been maimed or slaughtered. The days seem to only grow more macabre in nature, and I can't shake this feeling of foreboding cloud that the people and I share. Many simply wish for the ordeal to be over.",
                    "Date: 8th September, 1444",
                    "The war is won, and the people rejoice, but in spite of that, a miasma has swept the lands we have conquered. Perhaps this is judgment on the people who opposed us, or judgment for our aggressive expansion, but I cannot overlook the feeling of dread that was present throughout the campaign and it being the root of the evil which has caused this onslaught of death. This miasma seems to be creeping across the lands, yet for now, the lands around the capital seem to be spared. Hopefully, the Lord will grant us an era of respite and prosperity in the coming years.",
                    "Seems to end here",
                };
            }
        }

        if (gameObject.name == "Lore3")
        {
            int randomIndex = UnityEngine.Random.Range(0, 1);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["Lore3"] = new string[]
                {
                    "Queens Diary Entry Part One",
                    "Date: 29th February, 1444",
                    "My people go to war today, and I couldn't be prouder of them. We have strong moral fibre, and I am certain we will win against the savages on our border, expanding our splendour across the lands. I've been informed that the war will be over within a month, which seems far too long considering the difference in might between mighty Rúinhaven and the savages who mar our border. Then again, I was never well-versed in military affairs, so I should probably leave that to the king.",
                    "Date: 15th April, 1444",
                    "It's been a month, and this war continues. What is my king doing?! I was told these people are backwards and weaklings, yet the days pass and the war drags on. My king tells me that towns and hamlets are falling every day, but we have yet to break the spirit of our enemies, which concerns me because of the toll a long-term war could take on our realm and its people.",
                    "Seems to be another part",
                };
            }
        }

        if (gameObject.name == "Lore4")
        {
            int randomIndex = UnityEngine.Random.Range(0, 1);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["Lore4"] = new string[]
                {
                    "Queens Diary Entry Part Two",
                    "Date: 29th July, 1444",
                    "My concerns are seemingly becoming a reality, as the coffers are largely depleted, and a food shortage has begun. This weighs heavily on our farmers, as more food and supplies are needed for the war effort and for the realm, leading to higher quotas. I am told that the enemy has destroyed their own food in an attempt to slow our advances. Such savages! Can they not see they have lost? Why do they not just give up? More men have been conscripted for the front in an attempt to end the war more quickly. I just pray that the war ends now.",
                    "Date: 8th September, 1444",
                    "It has finally ended, and our people have returned, but they are not the same. Even our most cheerful knights have had the joy drained from them. How bad were things on the battlefield? Along with the loss of innocent spirits, our fields are now being besieged by a miasma that seems to destroy without prejudice, and I worry for the capital. My king seems shaken from this experience, and I can only hope that time will mend the damage done to our people and lands.",
                    "Seems to end here",
                };
            }
        }

        if (gameObject.name == "Lore5")
        {
            int randomIndex = UnityEngine.Random.Range(0, 1);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["Lore5"] = new string[]
                {
                    "Knights Diary Entry Part One",
                    "Date: 29th February, 1444",
                    "The call to war has begun, and upon my oath as a knight, I must answer the call! The king has rallied the men, and we now march upon our neighbours with the goal of expanding our borders to match our splendour. Our flags are raised high, and our honour knows no bounds. We are sure to win the hearts and minds of these people with a swift victory.",
                    "Date: 15th April, 1444",
                    "The first months of the war have not gone as I had expected. We were recently ambushed, and many knights were slain in the process. I see their faces from happier times, but now they lie buried in the ground. I held my childhood friend in my arms as he died, powerless to save him. Despite this, we were able to crush the remainder of their army, and now their towns are defenceless. I pray for the fallen and hope they have made their way to heaven, while I currently reside in hell.",
                    "Seems to be another part",
                };
            }
        }

        if (gameObject.name == "Lore6")
        {
            int randomIndex = UnityEngine.Random.Range(0, 1);//Creates a index with the possiblities equaling the amount of diagloue lines
            if (randomIndex == 0)
            {
                npcDialogues["Lore6"] = new string[]
                {
                    "Knights Diary Entry Part Two",
                    "Date: 29th July, 1444",
                    "Many go hungry as we march through their lands. These people have chosen to destroy their sources of food rather than bend the knee to our might. We have been ordered to slaughter them as we enter new towns and hamlets because the residents have made attempts on our lives. I killed a father in front of his children today. I can still see the horror on their faces. I fear my honour is slipping, but our nobles demand that we obey the orders given. We must not give up now that the capital is in sight. Take the capital, and we get to go home.",
                    "Date: 8th September, 1444",
                    "The capital burns, and looting is rampant. I get to go home, but my soul remains here. I am not the same person as the one who left for war. I have lost my honour in the slaughter of innocents, and my compassion has been crushed as I adapted to the darkness of this campaign. I doubt my family will even recognize the man who is set to return. I fear for my family, too, as a miasma seems to follow us back. Many of our nobles praise us for being part of history and urge us to revel in the glory, but I see no glory in this war.",
                    "Seems to end here",
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