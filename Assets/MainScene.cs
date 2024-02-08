using UnityEngine;

public class MainScene : MonoBehaviour
{
    void Start()
    {
        // Spawn characters at the positions provided by the character selection scene
        if (CharacterSelection.selectedCharacter != null)
        {
            Instantiate(CharacterSelection.selectedCharacter);
        }
    }
}
