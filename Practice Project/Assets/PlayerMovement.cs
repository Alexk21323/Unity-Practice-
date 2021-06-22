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
    bool jump = false;
    bool isJumping = false;
    bool crouch = false;

    public BoxCollider2D boxCol;
    public CircleCollider2D circCol;
    
    public Text ScoreText;

    int score; 
    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score:" + score.ToString();

        horziontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("moveSpeed", Mathf.Abs(horziontalMove));
        
        if(Input.GetButtonDown("Jump")) 
        {
            jump = true;
            isJumping = true;
            animator.SetBool("isJumping", isJumping); 

        }
        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if(Input.GetButtonUp("Crouch")){
            crouch = false;
        }

    }

    public void OnLanding()
    {
        isJumping = false;
        animator.SetBool("isJumping", isJumping); 
    }

    public void OnCrouching (bool crouch)
    {
        animator.SetBool("isCrouching", crouch);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            if (isJumping)
            {
                if(animator.GetBool("isJumping"))
                Destroy(col.gameObject);
                score++;
            }
            else
            {
                animator.SetBool("isHurt", true);    
                boxCol.enabled = false;
                circCol.enabled = false;
                controller.m_JumpForce = 300f;
                jump = true;            
            }
        }
        if(col.gameObject.tag =="Gem")
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
