using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOpossum : Enemy
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float moveSpeed = 5;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isLeft = true;
    private float leftX;
    private float rightX;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
    }

    void Update()
    {
        if (isLeft)
        {
            Debug.Log(isLeft);
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector3(3f, 3f, 1);
            if (transform.position.x < leftX)
            {
                isLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector3(-3f, 3f, 1);

            if (transform.position.x > rightX)
            {
                isLeft = true;
            }
        }
    }
}
