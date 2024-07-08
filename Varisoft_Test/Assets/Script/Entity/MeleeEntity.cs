using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntity : EntityBase
{
    public bool IsAttack;

    public override void AdditionBehaviour()
    {
        Melee();
    }

    private void Melee()
    {
        if (!IsAttack)
        {
            if(State == EntityState.Attack)
            {
                var _hp = Player.GetComponent<Health>();
                _hp.OnHpChange?.Invoke(Damage);

                IsAttack = true;
                StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3);
        IsAttack = false;
    }
}
