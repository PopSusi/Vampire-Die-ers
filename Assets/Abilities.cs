using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abilities : MonoBehaviour
{
    public abstract string description { get; set; }
    protected abstract float intervalBetweenAttacks { get; set; }
        protected abstract float damagePerAttack { get; set; }
        protected abstract float damagePerLevel { get; set; }
        protected abstract float speed { get; set; }
        protected abstract float speedPerLevel { get; set; }
        protected abstract float baseSpeed { get; set; }
        protected Animator anim;
    public int levelOfAbility;
    public enum EAbility { Onion = 0, Chain, Necronomicon};
    public abstract EAbility ability { get; set; }
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
