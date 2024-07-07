using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangedEntity : EntityBase
{
    [Header("Bullet")]
    [SerializeField] private GameObject magicBullet;


    public bool IsShoot;

    public override void Init(EntityData _entityData)
    {
        base.Init(_entityData);
        magicBullet = _entityData.Projectile;
    }

    public override void Behaviour()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (IsSaw)
        {
            if (State == EntityState.Attack)
            {
                if (!IsShoot)
                {
                    IsShoot = true;
                    Agent.canMove = false;

                    Rigidbody2D _magicBulletRB = Instantiate(magicBullet, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
                    Vector2 _direction = (Player.transform.position - transform.position).normalized;
                    _magicBulletRB.velocity = _direction * 4;
                    StartCoroutine(MagicShootCooldown());

                    CharacterSprite.SetDirectionWithAstar(Agent.desiredVelocity, false);
                    Destroy(_magicBulletRB.gameObject, 2);
                }
            }
        }
    }

    private IEnumerator MagicShootCooldown()
    {
        yield return new WaitForSeconds(2);
        IsShoot = false;
    }
}
