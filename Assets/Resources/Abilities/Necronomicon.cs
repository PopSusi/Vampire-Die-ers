using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Necronomicon : Abilities
{
    /*public override EAbility ability { get { return EAbility.Necronomicon; } set { ability = value; } }
    public override string description { get { return "The Book of The Dead. What could be a better suited tool for killing the undead?."; } set { description = value; } }
    protected override float intervalBetweenAttacks { get { return 0f; } set { intervalBetweenAttacks = value; } }
    protected override float damagePerAttack { get { return 1f; } set { damagePerAttack = value; } }
    protected override float damagePerLevel { get { return .5f; } set { damagePerLevel = value; } }
    protected override float speed { get { return 1f; } set { speed = value; } }
    protected override float speedPerLevel { get { return 2f; } set { speedPerLevel = value; } }
    protected override float baseSpeed { get { return 1f; } set { baseSpeed = value; } }*/

    Transform target;
    float angle;
    [SerializeField] float radius;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.gameObject.transform;
    }

    private void FixedUpdate()
    {
        float posX = target.position.x + Mathf.Cos(angle) * radius;
        float posY = target.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * type.speed;

        if (angle >= 360f)
            angle = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<EnemyBase>().TakeDamage(1f, Damageable.EDamage.Physical);
        }
    }
}
