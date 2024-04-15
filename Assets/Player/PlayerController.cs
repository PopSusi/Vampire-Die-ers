using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : Damageable
{
    public GameManager gm;
    public static PlayerController instance;
    
    public Healthbar healthBar;
    public Text scoreOut;
    public int scoreVal;
    
    [SerializeField] 
    private InputActionAsset actions;
    public InputAction moveAction;
    private Vector2 moveAmount;
    private float xp;
    [SerializeField] private float XP
    {
        get { return xp; }
        set { xp = value; XPUpdate(); }
    }

    public delegate void XPUpdateEvent(int xp);
    public event XPUpdateEvent XPUpdateUI;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        InitializeComponents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        Vector3 moveDir = new Vector3(moveAction.ReadValue<Vector2>().x, moveAction.ReadValue<Vector2>().y, 0);
        transform.Translate(moveDir * speed * Time.deltaTime, Space.Self);
        
    }

    public override void InitializeComponents()
    {
        actions.FindActionMap("Base").Enable();
        moveAction = actions.FindActionMap("Base").FindAction("Walk");
        moveAction.performed += OnMove;
        actions.FindActionMap("Base").FindAction("Pause").performed += OnPause;
        
        
        spriteControls = GetComponent<SpriteRenderer>();
        gm = GetComponent<GameManager>();
        
        healthBar.SetHealth((int) HP);
    }
    

    void OnMove(InputAction.CallbackContext context)
    {
        faceLeft = moveAction.ReadValue<Vector2>().x < 0 ; //If moving Left, faceLeft is false
        spriteControls.flipX = faceLeft; 
    }
    void OnPause(InputAction.CallbackContext context)
    {
        gm.OnPause();
    }
    public override void TakeDamage(float incomingDamage)
    {
        HP -= incomingDamage;
        /*switch (type)
        {
            case EDamage.Ranged:
                spriteControls.color += Color.black;
                break;
            case EDamage.Magic:
                spriteControls.color += Color.blue;
                break;
            case EDamage.Physical:
            default:
                spriteControls.color += Color.red;
                break;
        }*/
        healthBar.SetHealth((int) HP);
        Debug.Log(HP);
    }

    public void UpdateScore(int incoming)
    {
        scoreVal += incoming;
        scoreOut.text = "Score: " + scoreVal;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(other.gameObject.GetComponent<Projectiles>().damage);
            Destroy(other.gameObject);
        }
    }
    public void SubscribeToEnemyDeath(EnemyBase enemy)
    {
        enemy.enemyDeath += XPFunc;
    }
    private void XPFunc()
    {
        xp += 10;
    }
    private void XPUpdate()
    {
        //XP Modulo for new tempLevel
        //Update XP Bar
        //if tempLevel > level
            //level = tempLevel
            //XPUpdateUI?.Invoke;
    }
}
