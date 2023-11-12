using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CombatStats : ScriptableObject
{
    public float damage;
    public float attackSpeed;
    public float shotSpeed;
    public float attackRange;
    public int penetration;
}
