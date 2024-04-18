using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion : Abilities
{
    
    // Start is called before the first frame update
    void Awake()
    {
        ability = EAbility.Onion;
        description = "A wonderful spice used to scare off monsters of the night.";
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
