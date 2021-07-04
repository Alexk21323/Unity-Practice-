using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public playerController2D  controller;
    public Animator animator; 
    public float runSpeed = 40f; 
    float horziontalMove = 0f;
    float DisstanceToTheGround; 
    bool jump = false;
    bool isJumping = false;
    bool isFalling = false;
    bool crouch = false;

    public BoxCollider2D boxCol;
    public CircleCollider2D circCol;
    
    public Text ScoreText;

    int score; 

    void Start()
    {
        DisstanceToTheGround = circCol.bounds.extents.y;
    }
    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score:" + score.ToString();

        horziontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("moveSpeed", Mathf.Abs(horziontalMove));
        
        if(Input.GetButtonDown("Jump")) 
        {
            jump = true;
        }
        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if(Input.GetButtonUp("Crouch")){
            crouch = false;
        }
        
        if(controller.m_Grounded == false)
        {
            animator.SetBool("isJumping", true);
            isJumping = true;
        }
        
        if(controller.m_Rigidbody2D.velocity.y < 0f)
        {
            animator.SetBool("isFalling",true);
            animator.SetBool("isJumping", false);
        }

        if(controller.m_Grounded)
        {
            animator.SetBool("isFalling", false);
        }

    }

    public void OnLanding()
    {
        isJumping = false;
        animator.SetBool("isFalling", false); 
        
    }

    public void OnCrouching (bool crouch)
    {
        animator.SetBool("isCrouching", crouch);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            if (controller.m_Grounded == false)
            {
                Destroy(col.gameObject);
                score++;
            }
            else
            {
                animator.SetBool("isHurt", true);    
                controller.m_JumpForce = 400f;
                jump = true;
                circCol.enabled = false;
                boxCol.enabled = false;            
            }
        }
        if(col.gameObject.tag =="Gem" && animator.GetBool("isHurt") == false)
        {
            Destroy(col.gameObject);
            score++;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horziontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}