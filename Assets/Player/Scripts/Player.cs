    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    //components
    private Collider2D cd;
    private Rigidbody2D rb;
    public Healthbar healthbar;
    public Stamina stamina;

    //actions
    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private InputAction sprintAction;


    //movement
    private Vector2 movement;
    private float speed = 7.5f;
    private float sprintSpeed = 1.0f;
    private float sprintCost = 35.0f;
    public bool sprinting = false;
    public bool moving = false;
    public bool canSprint = true;

    //stamina regen
    private float staminaRegenDelay = 2.25f;     
    private float staminaRegenTimer = 0f;
    private float staminaRegenRate = 50f;

    //stamina
    public float maxStamina = 100.0f;
    public float currStamina;

    //hp
    private float maxHP = 100.0f;
    private float currHP;

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
            //reset the timer for stamina regen upon sprinting
            staminaRegenTimer = 0;
            //Debug.Log("Draining Stamina");

            //start draining stamina
            currStamina -= sprintCost * Time.deltaTime;
            //prevent stamina from going below 0
            currStamina = Mathf.Max(currStamina, 0);

            //when stamina does = 0 turn off canSprint and set sprintspeed to normals
            if(currStamina == 0)
            {
                canSprint = false;
                sprintSpeed = 1.0f;
            }
        }
        else
        {
            //stamina regen
            if (currStamina < maxStamina)
            {
                //timer += deltaTime (ticks for 2.25s)
                staminaRegenTimer += Time.deltaTime;

                //once 2.25s is up regen stamina
                if (staminaRegenTimer >= staminaRegenDelay)
                {
                    currStamina += staminaRegenRate * Time.deltaTime;

                    //set canSprint = true
                    canSprint = true;
                    //prevent stamina from exceeding maxStamina
                    currStamina = Mathf.Min(currStamina, maxStamina);
                }
            }
        }

    }

    //input actions
    public void Move(InputAction.CallbackContext c)
    {
        //moveInput reads input from c
        var movementInput = c.ReadValue<Vector2>();
        movement = movementInput;

        //if moveinput is more than (0, 0) then moving is true else false
        moving = movementInput != new Vector2(0, 0) ? true : false;
        
    }

    public void Sprint(InputAction.CallbackContext c)
    {
        //sprintInput reads input from c
        var sprintInput = c.ReadValue<float>();

        //if sprintInput > 0 and canSprint is true then sprintSpeed = 1.75f else sprintSpeed = 1.0f
        sprintSpeed = sprintInput > 0 && canSprint ? 1.75f : 1.0f;
        //if sprintInput > 0 then sprinting is true else false
        sprinting = sprintInput > 0 ? true : false;
    }

    public void Shoot(InputAction.CallbackContext c)
    {
        var shootInput = c.ReadValue<float>();
        Debug.Log(shootInput);
        Debug.Log("Shot a bullet");


        //logic:
        //changes the vector of projectile entity and then entity is cloned
        //the cloned entity will have vector and will therefor move 
    }



    //health related
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
