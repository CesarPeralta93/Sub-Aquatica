using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;
    public float lifetime;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 3) Destroy(this.gameObject);

        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Estalactitas")
        {
            Destroy(this.gameObject);
        }
    }
}
