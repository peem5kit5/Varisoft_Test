using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using Pathfinding;

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
    [SerializeField] private AIPath aiPath;
    [SerializeField] private Player player;
    private SpriteRenderer spriteRenderer;

    [Header("Status")]
    public string Name;
    public float Speed;
    public int Damage;
    public float AttackRange;
    public float SpotRange;
    public bool IsSaw;

    public EntityState State;
    public enum EntityState
    {
        Idle,
        Walk,
        Attack
    }

    public Action OnBehaviour;
    public CharacterSprite CharacterSprite => characterSprite;
    public AIPath Agent => aiPath;
    public Player Player => player;
    public Health Health => health;

    public virtual void Init(EntityData _entityData) 
    {
        OnBehaviour += AdditionBehaviour;
        OnBehaviour += CheckingPlayer;
        OnBehaviour += Movement;

        Name = _entityData.Name;
        Speed = _entityData.Speed;
        Damage = _entityData.Damage;
        AttackRange = _entityData.AttackRange;
        SpotRange = _entityData.SpotRange;

        deathParticle = _entityData.DeathParticle;

        aiPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();

        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        characterSprite = GetComponentInChildren<CharacterSprite>();

        anim.runtimeAnimatorController = _entityData.AnimatorController;

        player = GameManager.Instance.Player;

        health.DeathParticle = deathParticle;
        health.Init(_entityData.MaxHP);
        health.OnHpChange += OnDeath;

        spriteRenderer.color = _entityData.EntityColor;
        aiPath.maxSpeed = Speed;
    }

    public void ChangeStateMachine(EntityState _state) => State = _state;
    public void PlayAnimation(string _animationName) => anim.Play(_animationName);
    private void CheckingPlayer()
    {
        if (!IsSaw)
        {
            Collider2D[] _collider2D = Physics2D.OverlapCircleAll(transform.position, SpotRange);

            foreach (Collider2D _col in _collider2D)
            {
                if (_col.GetComponent<Player>() != null)
                {
                    Agent.destination = _col.transform.position;
                    ChangeStateMachine(EntityState.Walk);

                    IsSaw = true;
                }
            }
        }
    }

    public virtual void Movement()
    {
        if (IsSaw)
        {
            if (State == EntityState.Walk)
            {
                Agent.canMove = true;
                Agent.destination = Player.transform.position;
                CharacterSprite.SetDirectionWithAstar(Agent.desiredVelocity, true);
            }

            CheckingAttack();
        }
    }

    private void CheckingAttack()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) <= AttackRange)
            ChangeStateMachine(EntityState.Attack);
        else
            ChangeStateMachine(EntityState.Walk);
    }


    public abstract void AdditionBehaviour();

    private void Update()
    {
        if (GameManager.Instance.IsGamePlay)
            OnBehaviour?.Invoke();
        else
            Destroy(this);
    } 

    private void OnDeath(int _)
    {
        if (health.HP <= 0)
            GameManager.Instance.EntityDeath();
    }
}
