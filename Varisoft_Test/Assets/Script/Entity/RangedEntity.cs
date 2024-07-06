using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangedEntity : EntityBase
{
    [Header("Bullet")]
    [SerializeField] private GameObject magicBullet;

    public override void Init(EntityData _entityData)
    {
        base.Init(_entityData);
        magicBullet = _entityData.Projectile;
    }

    public override void Behaviour()
    {
        Movement();
        Shoot();
    }
    private void Movement()
    {
        if (State == EntityState.Walk)
        {

        }
        else if (State == EntityState.Idle)
        {

        }
    }

    private void Shoot()
    {
        if (State == EntityState.Attack)
        {

        }
    }
}
