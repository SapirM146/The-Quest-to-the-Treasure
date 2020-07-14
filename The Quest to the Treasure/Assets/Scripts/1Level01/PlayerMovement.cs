using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    public bool isStopped { get; set; }

    private void Start()
    {
        isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped)
        {
            movement.x = 0f;
            movement.y = 0f;
        }
        else
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.magnitude);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
