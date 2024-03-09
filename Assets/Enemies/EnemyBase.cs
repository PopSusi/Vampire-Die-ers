using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyBase : Damageable
{
    public GameObject playerObj; //Player Object in Scene, set by LevelManager - Singleton
    public PlayerController playerRef; //Player Script in Scene, set by LevelManager - Singleton
    public GameObject deathArea; //Can be empty
    public GameObject projectile; //Can be empty

    public bool projectileBased;
    public EDamage myType = EDamage.Physical; //Default Type
    public bool pollute; //If enemy leaves AOE after death
    private bool canAttack = true; //Able to attack - Allows us to use delay
    public float moveDelay; //How long before moving is set back to active after attack
    private bool canCollide = true;
    public Vector3 dirToPlayer; //Direction towards player
    private float distToPlayer; //How far from player - Float because Vector3.Distance().normalized returns float
    public Vector3 dirToGoal;
    public float closestToPlayer = 10; //When Enemy stops approaching
    public bool closeEnough; //Bool if enemy is close enough to the player to attack or do special stuff
    public bool moving; //Bool for if enemy can move, always opposite of closeEnough - Is own variable to override sometimes
    public bool avoidant;
    public bool rotator; //AVOIDANT AND ROTATOR MUST BE TRUE FOR ROTATION BEHAVIOUR
    private bool rotApproach = true;
    void Start()
    {        
        InitializeComponents(); //Set Animator and Sprite
        playerRef = playerObj.GetComponent<PlayerController>(); //Get Player Script
        Attack(); //Start with Attack
        StartCoroutine("UpdateDistance"); //Starts recursive check for Direction and Distance
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
        closeEnough = distToPlayer <= closestToPlayer;
        animControls.SetBool("moving", moving);
        MovementUpdate();
        yield return new WaitForSeconds(.5f);
        StartCoroutine("UpdateDistance");
    }

    private void Move()
    {
        if (moving)
        {
            transform.Translate(Time.deltaTime * speed * dirToGoal);
        }
    }

    private void MovementUpdate()
    {
        if (avoidant) //WANTS TO MOVE FROM PLAYER
        {
            if (closeEnough) 
            {
                if (!rotator)
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
                        Debug.DrawLine(transform.position, transform.position + dirToGoal * 5);
                        Debug.Log("GOING TO SIDE");
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
            if (projectileBased)
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
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        yield return wait;
        canAttack = true;
    }

    protected IEnumerator MoveDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(moveDelay);
        yield return wait;
        moving = true;
        //Debug.Log($"Moving set {moving} by MoveDelay");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && canCollide)
        {
            playerRef.TakeDamage(1);
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
        projectile.GetComponent<Projectiles>().playerObj = playerObj;
        //Debug.Log("Shooting");
    }

    protected override void Die()
    {
        if (pollute)
        {
            Instantiate(deathArea, this.transform.position, quaternion.identity);
        }
    }
}
