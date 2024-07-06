using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Health))]
public abstract class EntityBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Health health;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private CharacterSprite characterSprite;
    private SpriteRenderer spriteRenderer;

    [Header("Status")]
    public string Name;
    public float Speed;
    public int Damage;
    public float Range;

    public EntityState State;
    public enum EntityState
    {
        Idle,
        Walk,
        Attack
    }

    public Action OnBehaviour;
    public CharacterSprite CharacterSprite => characterSprite;

    public virtual void Init(EntityData _entityData) 
    {
        OnBehaviour += Behaviour;

        Name = _entityData.Name;
        Speed = _entityData.Speed;
        Damage = _entityData.Damage;
        Range = _entityData.Range;
        deathParticle = _entityData.DeathParticle;

        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        characterSprite = GetComponentInChildren<CharacterSprite>();

        health.OnHpChange += DeathParticleDeploy;

        spriteRenderer.color = _entityData.EntityColor;
    }

    public void ChangeStateMachine(EntityState _state) => State = _state;
    public void PlayAnimation(string _animationName) => anim.Play(_animationName);

    public abstract void Behaviour();

    private void Update() => OnBehaviour?.Invoke();

    private void DeathParticleDeploy()
    {
        if (health.HP <= 0 && deathParticle != null)
        {
            var _gameObject = Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(_gameObject, 2);
        }
    }
}
