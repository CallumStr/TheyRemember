using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float groundDist = 0.1f;
    public LayerMask terrainLayer;
    public CharacterController characterController;
    public SpriteRenderer sr;
    public AudioClip[] footstepSounds; // Footstep sounds array
    private AudioSource audioSource; // Audio source component
    public static PlayerController instance;

    private Animator animator;
    public float gravity = -9.81f; // Gravity force
    private Vector3 velocity; // Track gravity over time

    private bool isMoving; // Boolean to track if the character is moving

    // Reference to the Inventory script
    private Inventory playerInventory;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Get the player's inventory instance
        playerInventory = Inventory.instance;
    }

    void Update()
    {
        bool isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f; // Reset the y velocity if grounded
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move);

        // Determine if the player is moving based on the magnitude of the move vector
        isMoving = move.magnitude > 0.1f; // Set true if moving

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime; // Apply gravity when not grounded
        }

        characterController.Move((move * speed + new Vector3(0, velocity.y, 0)) * Time.deltaTime); // Apply movement + gravity

        HandleAnimations(move);

        // Play footstep sound if character is moving
        if (isMoving)
        {
            PlayFootstepSound();
        }

        // Log inventory contents when "P" is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (playerInventory != null)
            {
                Debug.Log("Inventory Contents:");
                foreach (KeyItem item in playerInventory.GetKeyItems())
                {
                    Debug.Log(item.itemName);
                }
            }
            else
            {
                Debug.LogWarning("Player inventory is null.");
            }
        }
    }

    private void HandleAnimations(Vector3 move)
    {
        animator.SetFloat("Speed", characterController.velocity.magnitude);
        sr.flipX = move.x < 0;
        animator.SetBool("MovingForward", move.z > 0);
        animator.SetBool("MovingBackward", move.z < 0);
    }

    private void PlayFootstepSound()
    {
        if (!audioSource.isPlaying && footstepSounds.Length > 0)
        {
            // Play footstep sounds when the player moves
            audioSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.Play();
        }
    }
}
