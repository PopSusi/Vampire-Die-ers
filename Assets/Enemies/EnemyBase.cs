using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyBase : Damageable
{
    public GameObject playerObj; //Player Object in Scene, set by LevelManager - Singleton
    public PlayerController playerRef; //Player Script in Scene, set by LevelManager - Singleton
    public EDamage myType = EDamage.Physical; //Default Type
    
    public Vector2 dirToPlayer;
    public Vector2 distToPlayer;
    public float closestToPlayer = 10; //When Enemy stops approaching
    public bool attackWhileMoving; //Bool to see if player fires or does melee while approaching
    void Start()
    {        
        InitializeComponents();
        playerRef = playerObj.GetComponent<PlayerController>();
        Attack();
        
    }

    void FixedUpdate()
    {
        CheckFlip();
    }

    public void Attack()
    {
        animControls.Play("Attack"); //Play anim with HitBox
        AttackDelay();
    }

    IEnumerator AttackDelay()
    {
        WaitForSeconds wait = new WaitForSeconds(attackDelay);
        yield return wait;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == playerObj)
        {
            playerRef.TakeDamage(1, myType);
        }
    }
    protected virtual void CheckFlip()
    {
        faceLeft = transform.position.x > playerObj.transform.position.x ? true : false ; //If moving Left, faceLeft is false
        spriteControls.flipX = faceLeft; 
        Debug.Log(faceLeft);
    }
}
