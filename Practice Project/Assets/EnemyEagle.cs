using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEagle : Enemy
{
    public Transform topPoint;
    public Transform bottomPoint;
    public float moveSpeed = 5;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isUp = true;
    private float topY;
    private float bottomY;
    private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        transform.DetachChildren();
        topY = topPoint.position.y;
        bottomY = bottomPoint.position.y;
    }

    void Update()
    {
       if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
                
            if (transform.position.y > topY)
            {
                isUp = false;
            }
        }
        else if (!isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
            
            if (transform.position.y < bottomY)
            {
                isUp = true;
            }
        }
        
        if(sw.ElapsedMilliseconds > (long)600)
            Destroy(gameObject);
    }
    
    public override void JumpOn()
    {
        anim.SetTrigger("death");   
        col.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        sw.Start();
    }
}
