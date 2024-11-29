using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Prota : MonoBehaviour
{

    private float horizontal;
    private float vertical;
    public float speed;
    public float vertspeed;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing = false;
    public float dashPower;
    public float dashDuration;
    public float dashCooldown;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private Transform groundlayer;

    private Transform lightHolder;
    private GameObject light;
    private Vector3 mousePos;
    private float time;
    private float timePassed;
    
    
    void Start()
    {
        canDash = true;
        lightHolder = this.gameObject.transform.GetChild(0);
        light = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
    }

    
    void Update()
    {
        if(isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        flip();

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        lightHolder.rotation = Quaternion.Euler(0, 0, rotZ);


        if(Input.GetButton("Jump") && canDash)
        {
            StartCoroutine(Dash());
            Debug.Log("space was pressed");
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(light.gameObject.activeSelf == true)
            {
                light.SetActive(false);
                
            }
            else
            {
                light.SetActive(true);
                StartCoroutine(LightBeam());
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, vertical * vertspeed).normalized;
    }

    private void flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            Vector3 lightholderlocalscale = lightHolder.localScale;
            localscale.x *= -1f;
            lightholderlocalscale.x *= -1f;
            transform.localScale = localscale;
            lightHolder.localScale = lightholderlocalscale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(horizontal * dashPower, vertical * dashPower);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator LightBeam()
    {
        time = 0;
        timePassed = 0;
        lightHolder.transform.localScale = new Vector3(0f, 0.5f, 1);
        while(lightHolder.transform.localScale.x != 4)
        {
            time = Time.deltaTime;
            timePassed = Time.deltaTime - time;
            float lightLength = lightHolder.transform.localScale.x;
            lightLength += time * 5;
            lightHolder.transform.localScale = new Vector3(lightLength, 0.5f, 1);
            yield return null;
        }
    }

    //private IEnumerator Dash()
    //{
    //    canDash = false;
    //    isDashing = true;
    //    rb.AddForce(transform.right * dashingPower);
    //    yield return new WaitForSeconds(dashingTime);
    //    isDashing = false;
    //    yield return new WaitForSeconds(dashingCooldown);
    //    canDash = true;
    //}
}
