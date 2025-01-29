using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roca : MonoBehaviour
{

    public Animator anim;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                anim.SetBool("Hit", true);
                Debug.Log("Roca Movida");
            }
        }
    }
}
