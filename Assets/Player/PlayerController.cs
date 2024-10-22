using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Concurrent;

public class PlayerController : Damageable
{
    public GameManager gm;
    public static PlayerController instance;
    
    public Healthbar healthBar;
    public TextMeshProUGUI scoreOut;
    public int scoreVal;
    
    [SerializeField] 
    private InputActionAsset actions;
    public InputAction moveAction;
    private Vector2 moveAmount;
    public float speed;
    private int xp;
    private int level = 1;

    public GameObject deathPanel;

    [SerializeField] private int XP
    {
        get { return xp; }
        set { xp = value; }
    }

    public delegate void XPUpdateEvent(int xp, int level);
    public event XPUpdateEvent XPUpdateUI;
    
    public HashSet<Abilities.EAbility> AbilitiesSO = new HashSet<Abilities.EAbility>();
    public ConcurrentDictionary<Abilities.EAbility, GameObject> AtoGO =
                       new ConcurrentDictionary<Abilities.EAbility, GameObject>();

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance= this;
        }
        else
        {
            Destroy(this);
        }
        AbilityType tempA = Resources.Load<AbilityType>("Abilities/AbilityTypes/Onion");
        AbilitiesSO.Add(tempA.ability);
        AtoGO.GetOrAdd(Abilities.EAbility.Onion, transform.GetChild(0).gameObject);

        EnemyBase.enemyDeath += XPFunc;
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
        animControls.SetBool("Moving", true);
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        gm.OnPause();
    }
    public void Pause()
    {
        gm.OnPause();
    }

    protected override void SubTakeDamage()
    {
        healthBar.SetHealth((int)HP);
        //Debug.Log(HP);
        animControls.SetTrigger("Hurt");
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
            TakeDamage(other.gameObject.GetComponent<Projectiles>().damage, EDamage.Magic);
            Debug.Log("Damage");
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1, EDamage.Physical);
            Debug.Log("Damage");
        }
    }
    /*public void SubscribeToEnemyDeath(EnemyBase enemy)
    {
        enemy.enemyDeath += XPFunc;
    }*/
    public void XPFunc()
    {
        XP += 10;
        Debug.Log($"Gained 10 XP");
        int req = CalcReq();
        if (xp > 50 * level)
        {
            level++;
            LevelUpCanvas.instance.GenerateAbilities(level);
            Debug.Log($"Level Up: {level} - Current XP: {XP}");
        }
        XPUpdateUI?.Invoke(xp, level);
    }

    protected override void Die()
    {
        base.Die();
        Time.timeScale = 0f;
        deathPanel.SetActive(true);
    }
    int CalcReq() => 20 * (level ^ 2);
}
