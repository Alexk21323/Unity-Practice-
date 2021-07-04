using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : Enemy
{
    public Transform leftPoint;
    public Transform rightPoint;
    private float moveSpeed = 3;
    private float jumpForce = 3;

    [Space]
    public LayerMask ground;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool faceLeft = true;
    private float leftX;
    private float rightX;

    private bool IsJumping
    {
        set => anim.SetBool("isJumping", value);
        get => anim.GetBool("isJumping");
    }

    private bool IsFalling
    {
        set => anim.SetBool("isFalling", value);
        get => anim.GetBool("isFalling");
    }

     protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsFalling);
        if (faceLeft)
        {
            if (col.IsTouchingLayers(ground))
            {
                IsJumping = true;
                rb.velocity = new Vector2(-moveSpeed, jumpForce);
            }

            if (transform.position.x < leftX && col.IsTouchingLayers(ground))
            {
                transform.localScale = new Vector3(-3f, 3f, 1);
                faceLeft = false;
            }
        }
        else
        {
            if (col.IsTouchingLayers(ground))
            {
                IsJumping = true;
                rb.velocity = new Vector2(moveSpeed, jumpForce);
            }

            if (transform.position.x > rightX && col.IsTouchingLayers(ground))
            {
                transform.localScale = new Vector3(3f, 3f, 1);
                faceLeft = true;
            }
        }

            if (rb.velocity.y < 0)
            {
                IsJumping = false;
                IsFalling = true;
            }

        if (col.IsTouchingLayers(ground))
        {
            IsFalling = false;
        }
    }
  
        
    
    

}
