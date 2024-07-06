using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string TargetTag;
    public int Damage;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag(TargetTag))
        {
            var _health = _collision.GetComponent<Health>();
            _health.DoDamage(Damage);

            Destroy(gameObject);
        }
        else if (_collision.CompareTag("Wall"))
            Destroy(gameObject);
    }
}
