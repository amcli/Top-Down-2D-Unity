    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    private Collider2D cd;
    private Rigidbody2D rb;

    private float maxHP = 100.0f;
    private float currHP;

    public Healthbar healthbar;
    public Stamina stamina;

    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private InputAction sprintAction;

    private Vector2 movement;

    private float speed = 7.5f;
    private float sprintSpeed = 1.0f;
    private float sprintCost = 35.0f;
    public bool sprinting = false;
    public bool moving = false;
    public bool canSprint = true;

    private float staminaRegenDelay = 2.25f;     
    private float staminaRegenTimer = 0f;
    private float staminaRegenRate = 50f;

    public float maxStamina = 100.0f;
    public float currStamina;

    // Start is called before the first frame update
    void Awake()
    {
        //get components
        playerInput = GetComponent<PlayerInput>();
        cd = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        //objects
        playerMovement = new PlayerMovement();

        //inputactions
        sprintAction = playerInput.actions["Sprint"];

        //hp initialization
        currHP = maxHP;
        healthbar.SetMaxHealth(maxHP);
        
        //stamina initialization
        currStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(movement.x * speed, movement.y * speed) * sprintSpeed;

        if (sprinting && canSprint && moving)
        {
            staminaRegenTimer = 0;
            Debug.Log("Draining Stamina");
            currStamina -= sprintCost * Time.deltaTime;
            currStamina = Mathf.Max(currStamina, 0);
            if(currStamina == 0)
            {
                canSprint = false;
                sprintSpeed = 1.0f;
            }
        }
        else
        {
            if (currStamina < maxStamina)
            {
                staminaRegenTimer += Time.deltaTime;

                if (staminaRegenTimer >= staminaRegenDelay)
                {
                    currStamina += staminaRegenRate * Time.deltaTime;
                    canSprint = true;
                    currStamina = Mathf.Min(currStamina, maxStamina);
                }
            }
        }

    }

    //input actions
    public void Move(InputAction.CallbackContext c)
    {
        var movementInput = c.ReadValue<Vector2>();
        movement = movementInput;
        //Debug.Log(movementInput);
        moving = movementInput != new Vector2(0, 0) ? true : false;
        
    }

    public void Sprint(InputAction.CallbackContext c)
    {
        var sprintInput = c.ReadValue<float>();
        Debug.Log(sprintInput);
        sprintSpeed = sprintInput > 0 && canSprint ? 1.75f : 1.0f;
        sprinting = sprintInput > 0 ? true : false;
        Debug.Log(sprinting);
    }

    public void Shoot(InputAction.CallbackContext c)
    {
        var shootInput = c.ReadValue<float>();
        Debug.Log(shootInput);
        Debug.Log("Shot a bullet");
    }








    //misc
    public void TakeDamage(float damage)
    {
        currHP -= damage;
        healthbar.SetHealth(currHP);

        if (currHP <=0)
        {
            GetComponent<Player>().enabled = false;
            Debug.Log("Died");
        }

    }
}
