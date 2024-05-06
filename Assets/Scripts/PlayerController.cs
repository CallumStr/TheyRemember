using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float groundDist = 0.1f;
    public LayerMask terrainLayer;
    public CharacterController characterController;
    public SpriteRenderer sr;

    public static PlayerController instance;

    private Animator animator;
    public float gravity = -9.81f; // Gravity force
    private Vector3 velocity; // to keep track of gravity over time

    // Reference to the Inventory script
    private Inventory playerInventory;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Get the player's inventory instance
        playerInventory = Inventory.instance;
    }

    void Update()
    {
        bool isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f; // reset the y velocity if grounded
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move);

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime; // apply gravity
        }
        
        characterController.Move((move * speed + new Vector3(0, velocity.y, 0)) * Time.deltaTime); // apply movement + gravity

        HandleAnimations(move);

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
        // Animation handling logic here
        animator.SetFloat("Speed", characterController.velocity.magnitude);
        sr.flipX = move.x < 0;
        animator.SetBool("MovingForward", move.z > 0);
        animator.SetBool("MovingBackward", move.z < 0);
    }
}
