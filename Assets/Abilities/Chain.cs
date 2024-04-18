using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : Abilities
{

    // Start is called before the first frame update
    void Start()
    {
        ability = EAbility.Chain;
        description = "A thick metal chain forged by the greated chainsmiths... is that really what we call them?";
        intervalBetweenAttacks = 0;
        damagePerAttack = 1;
        damagePerLevel = 2;
        speed = 1;
        speedPerLevel = .2f;
        baseSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
