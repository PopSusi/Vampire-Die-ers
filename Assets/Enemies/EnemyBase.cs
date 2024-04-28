using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyBase : Damageable
{
    private GameObject playerObj; //Player Object in Scene - Singleton
    private PlayerController playerRef; //Player Script in Scene - Singleton
    public GameObject deathArea; //Can be empty
    public GameObject projectile; //Can be empty
    public EnemyType type;

    private bool canAttack = true; //Able to attack - Allows us to use delay
    private bool canCollide = true;
    [HideInInspector] public Vector3 dirToPlayer; //Direction towards player
    private float distToPlayer; //How far from player - Float because Vector3.Distance().normalized returns float
    private Vector3 dirToGoal;
    private bool closeEnough; //Bool if enemy is close enough to the player to attack or do special stuff
    private bool moving; //Bool for if enemy can move, always opposite of closeEnough - Is own variable to override sometimes
    private bool rotApproach = true;
    public Attributes myAttributes;

    public delegate void DeathEvent();
    public event DeathEvent enemyDeath;

    private bool spawnedPollution;
    [System.Serializable] public struct Attributes {

        public bool projectileBased { get; set;}
        public bool pollute { get; set; }
        public bool avoidant { get; set; }
        public bool rotator { get; set; }
        public EDamage myType { get; set; }
        public string name { get; set; }

        /// <summary>
        /// Creates Attributes for an enemy type
        /// </summary>
        /// <param name="proj">Projectile Based?</param>
        /// <param name="poll">Polluting?</param>
        /// <param name="avoid">Avoidant?</param>
        /// <param name="rotate">Rotator?</param>
        /// <param name="type">Damage Type from EDamage</param>
        public Attributes(bool proj, bool poll, bool avoid, bool rotate, EDamage type, string newname)
        {
            projectileBased = proj;
            pollute = poll;
            avoidant = avoid;
            rotator = rotate;
            myType = type;
            name = newname;
        }
        public override string ToString()
        {
            string outPut = "";
            outPut += projectileBased.ToString() + ",\n";
            outPut += pollute.ToString() + ",\n";
            outPut += avoidant.ToString() + ",\n";
            outPut += rotator.ToString() + ",\n";
            outPut += myType.ToString();
            return outPut;
        }
    }
    
    void Start()
    {
        //myAttributes = new Attributes(type.projectileBased, type.pollute, type.avoidant, type.rotator, type.myType, type.enemyName);
        //DataSaver.instance.enemies.Add(this);
        //InitializeComponents(); //Set Animator and Sprite
        HP = 10;
        playerRef = PlayerController.instance; //Get Player Script
        playerObj = PlayerController.instance.gameObject;
        Attack(); //Start with Attack
        StartCoroutine("UpdateDistance"); //Starts recursive check for Direction and Distance
        PlayerController.instance.SubscribeToEnemyDeath(this);
        animControls = GetComponent<Animator>();
        animControls.runtimeAnimatorController = type.animations;
        spriteControls = GetComponent<SpriteRenderer>();
    }
    

    private void FixedUpdate()
    {
        CheckFlip();
        Move();
        if (closeEnough)
        {
            Attack();
        }
    }

    IEnumerator UpdateDistance() //Recursive function to get values about finding player
    {
        distToPlayer = Vector3.Distance(playerObj.transform.position, this.transform.position);
        closeEnough = distToPlayer <= type.closestToPlayer;
        animControls.SetBool("moving", moving);
        MovementUpdate();
        yield return new WaitForSeconds(.5f);
        StartCoroutine("UpdateDistance");
    }

    private void Move()
    {
        if (moving)
        {
            transform.Translate(Time.deltaTime * type.speed * dirToGoal);
        }
    }

    private void MovementUpdate()
    {
        if (myAttributes.avoidant) //WANTS TO MOVE FROM PLAYER
        {
            if (closeEnough) 
            {
                if (!myAttributes.rotator)
                {
                    dirToGoal = (transform.position - playerObj.transform.position)
                        .normalized; //DIR FROM PLAYER - AVOID
                }
                else
                {
                    if (rotApproach)
                    {
                        float dirMod = Random.value < 0.5f ? -1 : 1;
                        dirToGoal = (transform.position - playerObj.transform.position)
                            .normalized; //DIR FROM PLAYER AND 90 ROTATION - AVOID
                        dirToGoal = Quaternion.Euler(0, 0, 45 * dirMod) * dirToGoal;
                        //Debug.DrawLine(transform.position, transform.position + dirToGoal * 5);
                        //Debug.Log("GOING TO SIDE");
                        StartCoroutine("ApproachDelay");
                    }
                    else
                    {
                        dirToGoal = (transform.position - playerObj.transform.position)
                            .normalized;
                        Debug.Log("APPROACH");
                    }
                }
            } else
            {
                dirToGoal = (playerObj.transform.position - transform.position).normalized; //DIR TO PLAYER - APPROACH
            }
        }
        else //WANTS TO MOVE TO PLAYER
        {
            dirToGoal = (playerObj.transform.position - transform.position).normalized; //DIR TO PLAYER - FOLLOW
        }
    }

    IEnumerator ApproachDelay()
    {
        yield return new WaitForSeconds(3f);
        rotApproach = true;
    }
    protected virtual void Attack()
    {
        moving = false;
        //Debug.Log($"Moving set {moving} by Attack");
        if (canAttack)
        {
            //Debug.Log("Attacking");
            if (myAttributes.projectileBased)
            {
                animControls.Play("Attack"); //Play anim with HitBox
                Shoot();
            }
            else
            {
                animControls.Play("Attack"); //Play anim with HitBox
                //Debug.Log("Punching");
            }
            canAttack = false;
            StartCoroutine("AttackDelay");
        }

        StartCoroutine("MoveDelay");
    }
    protected IEnumerator AttackDelay()
    {
        //Debug.Log("Waiting to attack");
        WaitForSeconds wait = new WaitForSeconds(type.attackDelay);
        yield return wait;
        canAttack = true;
    }

    protected IEnumerator MoveDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(type.moveDelay);
        yield return wait;
        moving = true;
        //Debug.Log($"Moving set {moving} by MoveDelay");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && canCollide)
        {
            playerRef.TakeDamage(1, EDamage.Physical);
            canCollide = false;
            ColliderDelay();
        }
    }
    protected IEnumerator ColliderDelay()
    {
        yield return new WaitForSeconds(.1f);
        canCollide = true;
    }
    protected void CheckFlip()
    {
        faceLeft = transform.position.x > playerObj.transform.position.x ? true : false ; //If moving Left, faceLeft is false
        spriteControls.flipX = faceLeft;
    }

    protected void Shoot()
    {
        GameObject myProj = Instantiate(projectile, this.transform.position, quaternion.identity);
        //Debug.Log("Shooting");
    }

    protected override void Die()
    {
        enemyDeath?.Invoke();
        //Debug.Log("Supposed to be dead");
        if (myAttributes.pollute && spawnedPollution)
        {
            Instantiate(deathArea, this.transform.position, quaternion.identity);
            spawnedPollution = true;
        }
        Destroy(gameObject);
    }
    protected override void SubTakeDamage()
    {
        //Debug.Log($"Damage from Player");
    }
}
