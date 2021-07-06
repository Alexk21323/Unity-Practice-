using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public virtual void JumpOn()
    {
        anim.SetTrigger("death");   
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
