using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necronomicon : Abilities
{
    // Start is called before the first frame update
    void Start()
    {
        ability = EAbility.Necronomicon;
        description = "The Book of The Dead. What could be a better suited tool for killing the undead?";
        intervalBetweenAttacks = 0;
        damagePerAttack = 1;
        damagePerLevel = .5f;
        speed = 1;
        speedPerLevel = .2f;
        baseSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
