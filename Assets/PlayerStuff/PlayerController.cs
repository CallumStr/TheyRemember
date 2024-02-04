using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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

        Vector3 moveDir = new Vector3(x, 0, z);
        rb.velocity = moveDir * speed;

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

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
