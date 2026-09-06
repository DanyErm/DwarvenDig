using UnityEngine;
using Zenject;

public class LadderMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [Inject] GameSettings _gameSettings;

    private bool isLadder;
    private bool isClimbing;
    private float originalGravityScale;
    private float verticalInput;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        originalGravityScale = rb.gravityScale;
    }

    private void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        if (isLadder && Mathf.Abs(verticalInput) > 0.01f)
        {
            isClimbing = true;
        }
        else if (Mathf.Abs(verticalInput) < 0.01f)
        {
            isClimbing = false;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalInput * _gameSettings.ClimbSpeed);
        }
        else
        {
            if (isLadder)
            {
                rb.gravityScale = originalGravityScale;
            }
            else
            {
                if (!Mathf.Approximately(rb.gravityScale, originalGravityScale))
                {
                    rb.gravityScale = originalGravityScale;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            rb.gravityScale = originalGravityScale;
        }
    }
}