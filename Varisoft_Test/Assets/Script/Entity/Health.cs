using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Status")]
    public int HP;
    public int MaxHP;

    public Action OnHpChange;

    public void Init(int _maxHP)
    {
        OnHpChange += Death;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        MaxHP = _maxHP;
        HP = MaxHP;
    }

    public void DoDamage(int _damage)
    {
        HP -= _damage;
        //StartCoroutine(FadingColor());
        OnHpChange?.Invoke();
    }

    private void Death()
    {
        if(HP <= 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
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
