using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EntityData")]
public class EntityData : ScriptableObject
{
    public string Name;
    public int MaxHP;
    public Color EntityColor;

    public float Speed;
    public int Damage;
    public float AttackRange;
    public float SpotRange;

    public GameObject DeathParticle;

    [Header("If it ranged or have Projecttile.")]
    public GameObject Projectile;
}
