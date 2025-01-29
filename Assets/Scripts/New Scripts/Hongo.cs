using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hongo : MonoBehaviour
{

    public Animator anim;
    public Animator puerta;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "OrbesdeLuz")
        {
            anim.SetBool("Hit", true);
            puerta.SetBool("Hit", true);
            StartCoroutine(reset());
            Debug.Log("Hongo Hit");
        }
    }

    private IEnumerator reset()
    {
        yield return new WaitForSeconds(5f);
        anim.SetBool("Hit", false);
        puerta.SetBool("Hit", false);
    }
}
