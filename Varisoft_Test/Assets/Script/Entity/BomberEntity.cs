using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEntity : EntityBase
{
    public override void Init(EntityData _entityData)
    {
        base.Init(_entityData);
    }

    public override void AdditionBehaviour()
    {
        Bomber();
    }

    private void Bomber()
    {
        if (IsSaw)
        {
            if(State == EntityState.Attack)
            {
                var _hp = Player.GetComponent<Health>();
                _hp.OnHpChange?.Invoke(Damage);

                Health.Death();
            }
        }
    }
}
