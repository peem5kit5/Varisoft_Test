using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject deathParticle;

    [Header("Status")]
    public int HP;
    public int MaxHP;

    public Action OnHpChange;

    public void Init(int _maxHP)
    {
        deathParticle = 
        OnHpChange += Death;

        MaxHP = _maxHP;
        HP = MaxHP;
    }

    public void DoDamage(int _damage)
    {
        HP -= _damage;
        OnHpChange.Invoke();
    }

    private void Death()
    {
        if(HP <= 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
