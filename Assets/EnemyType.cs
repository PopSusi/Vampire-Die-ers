using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyType: ScriptableObject
{
    public Damageable.EDamage myType = Damageable.EDamage.Physical; //Default Type
    public bool projectileBased;
    public bool pollute; //If enemy leaves AOE after death
    public bool avoidant;
    public bool rotator; //AVOIDANT AND ROTATOR MUST BE TRUE FOR ROTATION BEHAVIOUR
    public float closestToPlayer = 10; //When Enemy stops approaching
    public float moveDelay;
    public float speed;
    public float attackDelay;
    public AnimatorOverrideController animations;
}
