using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class AbilityType : ScriptableObject
{
    public string description;
    public float intervalBetweenAttacks;
    public float damagePerAttack;
    public float damagePerLevel;
    public float speed;
    public float speedPerLevel;
    public float baseSpeed;
    public Abilities.EAbility ability;
    public GameObject prefab;
    public AnimatorOverrideController overrideController;
}
