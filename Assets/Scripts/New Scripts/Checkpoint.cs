using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    Blue blue;

    private void Awake()
    {
        blue = GameObject.FindGameObjectWithTag("Player").GetComponent<Blue>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            blue.UpdateCheckpoint(transform.position);
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
