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
    protected LayerMask layerMask = 10;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        if (intervalBetweenAttacks > 0) {
         StartCoroutine("Attackinterval");
        }
        else
        {
            AttackAnim();
        }
    }

    IEnumerator AttackInterval()
    {
        AttackAnim();
        AttackSub();
        WaitForSeconds wait = new WaitForSeconds(intervalBetweenAttacks);
        yield return wait;
        StartCoroutine("AttackInterval");
    }

    private void AttackAnim()
    {
        anim.SetTrigger("AttackAnimPlay");
    }
    protected virtual void AttackSub()
    {
        Debug.LogError($"AttackSub not implemented on {gameObject.name}");
    }
    private void LevelUp()
    {
        levelOfAbility++;
        speed++;
        damagePerAttack += damagePerLevel;
        anim.speed = speed / baseSpeed;
    }
}
