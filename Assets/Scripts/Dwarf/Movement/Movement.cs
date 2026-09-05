using UnityEngine;

public class Movement
{
    private bool isGrounded;
    

    public void Walk(Rigidbody2D rb, float walkVelocity)
    {
        rb.linearVelocityX = walkVelocity;
    }


    public void Jump(Rigidbody2D rb, float jumpVelocity, Transform groundCheck, Vector2 checkBoxSize, LayerMask groundLayer)
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, checkBoxSize, 0, groundLayer);

        if (isGrounded)
        {
            rb.linearVelocityY = jumpVelocity;
        }
    }


    public void CutJump(Rigidbody2D rb, float jumpCutMultiplier)
    {
        if (rb.linearVelocityY > 0)
        {
            rb.linearVelocityY *= jumpCutMultiplier;
        }
    }
}