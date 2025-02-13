using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Blue : MonoBehaviour
{
    PlayerInput input;
    InputAction move;
    InputAction fireLightOrb;
    InputAction lightpulse;
    InputAction pauseGame;
    InputAction bubble;
    public Animator anim;
    public Rigidbody2D rb;

    Vector2 checkpointPos;

    public GameObject HUD;
    public GameObject pauseCanvas;
    public GameObject deathCanvas;
    public bool isPaused = false;

    public GameObject lightOrb, lightPulse, burbuja;
    public Transform lightOrbFirePoint;

    public bool canDash, isDashing;
    public float dashPower;
    public float dashTime;

    public bool canLightOrb, isLightOrbing;
    public float lightOrbCooldown;

    public bool canBubble, isBubbleCooldown;

    private Vector3 mousePos;
    public Vector3 axis;

    [SerializeField] float speed;

    public float horizontal;
    public float vertical;

    private Interactuable interactuable;


    public Image burbujaCooldown;

    public Image orbeDeLuzCooldown;

    public Image pulsoCooldown;




    void Start()
    {
        Debug.Log("mviendo");
        input = GetComponent<PlayerInput>();
        move = input.actions.FindAction("Move");
        canDash = true;
        canBubble = true;
        checkpointPos = transform.position;
    }


    void Update()
    {
        burbujaCooldown.fillAmount += 0.1f * Time.deltaTime;
        orbeDeLuzCooldown.fillAmount += 0.33f * Time.deltaTime;
        pulsoCooldown.fillAmount += 0.25f * Time.deltaTime;

        MovePlayer(false);

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        axis = new Vector3(horizontal, 0, vertical);


        if (horizontal != 0 || vertical != 0)
        {
            anim.SetBool("Moving", true);
            if (axis.x != 0)
            {
                Quaternion rotationPlayer;
                if (axis.x > 0) rotationPlayer = Quaternion.Euler(new Vector3(0, 0, 0));
                else rotationPlayer = Quaternion.Euler(new Vector3(0, 180, 0));
                transform.rotation = rotationPlayer;
            }
        }
        else
        {
            anim.SetBool("Moving", false);
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        lightOrbFirePoint.rotation = Quaternion.Euler(0, 0, rotZ);

    }

    public void Dashing()
    {
        StartCoroutine(Dash());
    }

    public IEnumerator LightOrbingCooldown()
    {
        canLightOrb = false;
        yield return new WaitForSeconds(3f);
        canLightOrb = true;
    }

    public IEnumerator LightOrbCooldown()
    {
        orbeDeLuzCooldown.fillAmount += Time.deltaTime;
        yield return null;
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(horizontal * dashPower, vertical * dashPower);
        yield return new WaitForSeconds(dashTime);
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        isDashing = false;
        yield return new WaitForSeconds(5);
        canDash = true;
    }

    public void MovePlayer(bool cancel)
    {
        if (cancel) return;
        Vector2 direction = move.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        if(direction.x != 0 || direction.y != 0)
        {
            anim.SetBool("Moving", true);
        }
    }

    public void Bubble(InputAction.CallbackContext context)
    {
        burbujaCooldown.fillAmount = 0;
        if(canBubble == true)
        {
            if(context.performed)
            {
                Instantiate(burbuja, lightOrbFirePoint.position, lightOrbFirePoint.rotation);
            }
            else
            {
                return;
            }
        }
    }

    public void LightOrb(InputAction.CallbackContext context)
    {
        orbeDeLuzCooldown.fillAmount = 0;
        if(canLightOrb == true)
        {
            if (context.performed)
            {
                Instantiate(lightOrb, lightOrbFirePoint.position, lightOrbFirePoint.rotation);
            }
            else
            {
                return;
            }
        }
        //LightOrbCooldown();
        //StartCoroutine(LightOrbCooldown());
        StartCoroutine(LightOrbingCooldown());
    }

    public void LightPulse(InputAction.CallbackContext context)
    {
        pulsoCooldown.fillAmount = 0;
        if(context.performed)
        {
            anim.SetTrigger("PulsoDeLuz");
        }
    }

    public void Interactuar(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            
        }
    }

    public void Activar(InputAction.CallbackContext context)
    {
        if(interactuable != null)
        {
            Debug.Log("Se activo");
            interactuable.Activar();
        }
    }

    public void Pausar(InputAction.CallbackContext context)
    {
        if(isPaused == false)
        {
            pauseCanvas.SetActive(true);
            isPaused = true;
            HUD.SetActive(false);
            Time.timeScale = 0;
        }
        else if(isPaused = true)
        {
            pauseCanvas.SetActive(false);
            isPaused = false;
            HUD.SetActive(true);
            Time.timeScale = 1;
        }
    }

    private IEnumerator BounceSpeedCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            bool cancel = true;
            MovePlayer(cancel);
            float speed = rb.velocity.magnitude;
            float bounceSpeed = 5;
            bounceSpeed -= Time.deltaTime;
            //StartCoroutine(BounceSpeedCooldown(bounceSpeed));
            //Vector3 direction = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
            Vector2 direction = collision.contacts[0].normal;
            rb.AddForce(direction * bounceSpeed, ForceMode2D.Impulse);
            StartCoroutine(BounceSpeedCooldown());
            cancel = false;
            MovePlayer(cancel);
            //rb.velocity = Vector2.zero;
            //rb.velocity = direction * Mathf.Max(speed * 50f - Time.deltaTime, 0f);
            Debug.Log("Rebote");
            Debug.Log(direction);
            Debug.Log(speed);
            Debug.Log(bounceSpeed);
            Debug.DrawRay(collision.contacts[0].point, direction, Color.red, 10);
        }
        if(collision.gameObject.tag == "Estalactitas")
        {
            Die();
        }
        if(collision.gameObject.tag == "Hollum")
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            interactuable = collision.GetComponent<Interactuable>();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            interactuable = null;
        }
    }

    public void Die()
    {
        deathCanvas.SetActive(true);
    }

    public void Respawn()
    {
        StartCoroutine(Respawn(1));
    }

    public IEnumerator Respawn(float respawnDuration)
    {
        rb.velocity = new Vector2(0, 0);
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(respawnDuration);
        deathCanvas.SetActive(false);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }
}