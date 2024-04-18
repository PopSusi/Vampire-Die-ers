using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    [SerializeField] protected float intervalBetweenAttacks, damagePerAttack, damagePerLevel, speed, speedPerLevel, baseSpeed;
    protected Animator anim;
    public int levelOfAbility;
    public enum EAbility { Onion, Chain, Necronomicon};
    public EAbility ability = EAbility.Onion;
    public string description = " ";

    // Start is called before the first frame update
    void Start()
    {
        if (intervalBetweenAttacks > 0) {
         StartCoroutine("Attackinterval");
        }
        else
        {
            Attack();
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
        damagePerAttack += damagePerLevel;
        anim.speed = speed / baseSpeed;
    }
}
