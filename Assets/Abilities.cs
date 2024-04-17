using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    protected float intervalBetweenAttacks, damagePerAttack, speed, baseSpeed;
    protected Animator anim;
    public int levelOfAbility;
    protected enum Type { Interval, Continuous };
    [SerializeField] protected Type myType = Type.Interval;
    protected enum EAbility { Onion, Chain, Necronomican};
    [SerializeField] protected EAbility ability = EAbility.Onion;

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        if (myType == Type.Interval) {
         StartCoroutine("Attackinterval");
        }
    }

    IEnumerator AttackInterval()
    {
        Attack();
        WaitForSeconds wait = new WaitForSeconds(intervalBetweenAttacks);
        yield return wait;
        StartCoroutine("AttackInterval");
    }

    private void Attack()
    {
        anim.SetTrigger("AttackAnimPlay");
    }
    private void LevelUp()
    {
        levelOfAbility++;
        speed++;
        anim.speed = speed / baseSpeed;
    }
}
