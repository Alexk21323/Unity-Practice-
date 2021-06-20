using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public playerController2D  controller;
    
    public float runSpeed = 40f; 
    float horziontalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        horziontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            Debug.Log("hit");
        }
    }

    
    void FixedUpdate()
    {
        controller.Move(horziontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
