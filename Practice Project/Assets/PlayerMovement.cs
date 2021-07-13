using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
 
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public playerController2D  controller;
    public Animator animator; 
    public float runSpeed = 30f; 
    float horziontalMove = 0f;
    float DisstanceToTheGround; 
    bool jump = false;
    bool crouch = false;
 
    public BoxCollider2D boxCol;
    public CircleCollider2D circCol;
 
    public UnityEvent DeathEvent;
    
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
        else if(Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        
        if(controller.m_Grounded == false)
        {
            animator.SetBool("isJumping", true);
        }
        
        if(controller.m_Rigidbody2D.velocity.y < -0.01f)
        {
            animator.SetBool("isFalling",true);
            animator.SetBool("isJumping", false);
        }
 
        if(controller.m_Grounded)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);

        }
        
        if(transform.position.y < -7)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        }
 
    }
 
    public void OnLanding()
    {
        animator.SetBool("isFalling", false);    
    }
 
    public void OnCrouching (bool crouch)
    {
        animator.SetBool("isCrouching", crouch);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy" && !(animator.GetBool("isHurt")))
        {
            if (controller.m_Grounded == false && controller.m_Rigidbody2D.velocity.y < 0f)
            {
                Enemy enemy = col.gameObject.GetComponent<Enemy>();
                enemy.JumpOn();
                score++;
                controller.m_Rigidbody2D.velocity = new Vector2(0f, 0f);
                controller.m_Rigidbody2D.AddForce(new Vector2(0f, 300f));
            }
            else
            {
                animator.SetBool("isHurt", true);    
                controller.m_Rigidbody2D.velocity = new Vector2(0f, 0f);
                controller.m_Rigidbody2D.AddForce(new Vector2(0f, 700f));
                circCol.enabled = false;
                boxCol.enabled = false;            
                DeathEvent.Invoke();
            }
        }
        if(col.gameObject.tag =="Gem" && animator.GetBool("isHurt") == false)
        {
            Destroy(col.gameObject);
            score++;
        }
 
        if(col.gameObject.tag =="Cherry" && animator.GetBool("isHurt") == false)
        {
            Destroy(col.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        }
    }
 
    void FixedUpdate()
    {
        controller.Move(horziontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}

