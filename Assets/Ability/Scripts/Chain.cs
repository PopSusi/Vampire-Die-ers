using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : Abilities
{
    /*public override EAbility ability { get { return EAbility.Necronomicon; } set { ability = value; } }
    public override string description { get { return "A thick metal chain forged by the greated chainsmiths... is that really what we call them?"; } set { description = value; } }
    protected override float intervalBetweenAttacks { get { return 2f; } set { intervalBetweenAttacks = value; } }
    protected override float damagePerAttack { get { return 1f; } set { damagePerAttack = value; } }
    protected override float damagePerLevel { get { return 2f; } set { damagePerLevel = value; } }
    protected override float speed { get { return 1f; } set { speed = value; } }
    protected override float speedPerLevel { get { return .2f; } set { speedPerLevel = value; } }
    protected override float baseSpeed { get { return 1f; } set { baseSpeed = value; } }*/
    // Start is called before the first frame update

    private void Start()
    {
        StartCoroutine("Delay");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3 / type.speed);
        anim.SetTrigger("Attack");
    }
}
