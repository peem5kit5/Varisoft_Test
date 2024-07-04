using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Health))]
public abstract class EntityBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Health health;

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

    public virtual void Init(EntityData _entityData) 
    {
        OnBehaviour += Behaviour;

        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

        anim = _entityData.Anim;
        sprite = _entityData.Sprite;

        Name = _entityData.Name;
        Speed = _entityData.Speed;
        Damage = _entityData.Damage;
        Range = _entityData.Range;
    }
    public virtual void ChangeStateMachine(EntityState _state) => State = _state;
    public void PlayAnimation(string _animationName) => anim.Play(_animationName);

    public abstract void Behaviour();
}
