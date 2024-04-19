using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion : Abilities
{
    public override EAbility ability { get { return EAbility.Onion; } set { ability = value; } }
    public override string description { get { return "A wonderful spice used to scare off monsters of the night."; } set { description = value; } }
    protected override float intervalBetweenAttacks { get { return 0f; } set { intervalBetweenAttacks = value; } }
    protected override float damagePerAttack { get { return 1f; } set { damagePerAttack = value; } }
    protected override float damagePerLevel { get { return 2f; } set { damagePerLevel = value; } }
    protected override float speed { get { return 1f; } set { speed = value; } }
    protected override float speedPerLevel { get { return .2f; } set { speedPerLevel = value; } }
    protected override float baseSpeed { get { return 1f; } set { baseSpeed = value; } }

    // Update is called once per frame
    private void DamageBurst()
    {
        Collider2D[] possibles = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x, layerMask);
        foreach (Collider2D colliderCheck in possibles) {
            if (colliderCheck.gameObject.CompareTag("Enemy"))
            {
                colliderCheck.gameObject.GetComponent<EnemyBase>().TakeDamage(1f, Damageable.EDamage.Magic);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<EnemyBase>().TakeDamage(1f, Damageable.EDamage.Magic);

        }
    }
}
