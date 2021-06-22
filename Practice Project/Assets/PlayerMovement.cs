using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("hit");
            if (isJumping)
            {
                if(animator.GetBool("isJumping"))
                Destroy(col.gameObject);
            }
            else
            {
            Destroy(this.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horziontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
