using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;
    public LayerMask terrainLayer;
    public CharacterController characterController;
    public SpriteRenderer sr;

    public static PlayerController instance;

    private Animator animator;

    // Slope variables
    public float slopeForce = 10f;
    public float slopeRayLength = 1.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;

        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Check if moving forward (positively on the Z-axis)
        bool movingForward = z > 0;

        // Check if moving backward (negatively on the Z-axis)
        bool movingBackward = z < 0;

        Vector3 moveDir = new Vector3(x, 0, z).normalized;

        // Cast a ray to check the ground slope
        RaycastHit slopeHit;
        Physics.Raycast(transform.position, -transform.up, out slopeHit, Mathf.Infinity, terrainLayer);

        // If on a slope, adjust the player's movement based on the slope angle
        if (slopeHit.collider != null && slopeHit.normal != Vector3.up)
        {
            float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);

            // Apply consistent downward force to move downwards on slopes
            Vector3 slopeMove = Vector3.down * slopeForce;
            characterController.Move(slopeMove * Time.deltaTime);
        }

        characterController.Move(moveDir * speed * Time.deltaTime);

        animator.SetFloat("Speed", Mathf.Abs(characterController.velocity.x));

        // Flip the sprite when moving left
        if (x < 0)
        {
            sr.flipX = true;
        }
        // Flip the sprite when moving right
        else if (x > 0)
        {
            sr.flipX = false;
        }

        // Set conditions for moving forward and backward animations
        animator.SetBool("MovingForward", movingForward);
        animator.SetBool("MovingBackward", movingBackward);
    }
}
