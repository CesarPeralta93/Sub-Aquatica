using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Movement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Movement()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1.5f);
        rb.AddForce(-transform.up * speed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Movement());
    }
}
