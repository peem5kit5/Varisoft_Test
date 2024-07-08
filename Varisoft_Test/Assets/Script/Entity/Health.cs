using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header("References")]
    public GameObject DeathParticle;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Status")]
    public int HP;
    public int MaxHP;

    public Action<int> OnHpChange;

    public void Init(int _maxHP)
    {
        OnHpChange += DoDamage;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        MaxHP = _maxHP;
        HP = MaxHP;
    }

    public void DoDamage(int _damage)
    {
        HP -= _damage;
        StartCoroutine(FadingColor());

        if(HP <= 0)
            Death();

        //OnHpChange?.Invoke();
    }

    public void Death()
    {
        GameObject _gameObject = Instantiate(GameManager.Instance.
            DeathParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private IEnumerator FadingColor()
    {
        Color _oldColor = spriteRenderer.color;
        Color _newColor = spriteRenderer.color;
        _newColor.a = 50;
        spriteRenderer.color = _newColor;
        yield return new WaitForSeconds(1);
        spriteRenderer.color = _oldColor;
    }
}
