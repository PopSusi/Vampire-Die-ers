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
    private int xp;
    private int level;
    [SerializeField] private int XP
    {
        get { return xp; }
        set { xp = value; }
    }

    public delegate void XPUpdateEvent(int xp, int level);
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
        actions.FindAction("XP++").performed += XPUP;
    }
    private void XPUP(InputAction.CallbackContext context)
    {
        XPFunc();
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

    protected override void SubTakeDamage()
    {
        healthBar.SetHealth((int)HP);
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
            TakeDamage(other.gameObject.GetComponent<Projectiles>().damage, EDamage.Physical);
            Destroy(other.gameObject);
        }
    }
    public void SubscribeToEnemyDeath(EnemyBase enemy)
    {
        enemy.enemyDeath += XPFunc;
    }
    private void XPFunc()
    {
        XP += 100;
        if(xp > (20 * level ^ 2))
        {
            level++;
            LevelUpCanvas.instance.GenerateAbility(level);
            Debug.Log($"Level Up: {level} - Current XP: {XP}");
        }
        XPUpdateUI?.Invoke(xp, level);
    }
}
