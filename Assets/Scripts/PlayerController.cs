using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float groundDist = 0.1f;
    public LayerMask terrainLayer;
    public CharacterController characterController;
    public SpriteRenderer sr;
    public Inventory inventory; // Reference to the Inventory script

    public float pickupRange = 2f; // Define the pickup range here

    private Animator animator;
    public float gravity = -9.81f; // Gravity force
    private Vector3 velocity; // to keep track of gravity over time

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupKeyItem();
        }
    }

    private void TryPickupKeyItem()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange); // Adjust pickupRange as needed
        foreach (Collider collider in colliders)
        {
            KeyItemPickup keyItemPickup = collider.GetComponent<KeyItemPickup>();
            if (keyItemPickup != null && keyItemPickup.canPickup)
            {
                // Add the key item to the player's inventory
                inventory.AddKeyItem(keyItemPickup.keyItem);
                // Deactivate the key item GameObject after picking it up
                keyItemPickup.gameObject.SetActive(false);
                // Update hotbar display after picking up the key item (if needed)
                // ...

                Debug.Log("Key item picked up.");
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
