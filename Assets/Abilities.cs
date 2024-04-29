using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abilities : MonoBehaviour
{
    public AbilityType type;
    public int levelOfAbility;
    public enum EAbility { Onion = 0, Chain, Necronomicon};
    protected LayerMask layerMask = 10;
    protected Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.runtimeAnimatorController = type.overrideController;
    }
    void Start()
    {
        if (type.intervalBetweenAttacks > 0) {
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
        WaitForSeconds wait = new WaitForSeconds(type.intervalBetweenAttacks);
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
        type.speed++;
        type.damagePerAttack += type.damagePerLevel;
        anim.speed = type.speed / type.baseSpeed;
    }
}
