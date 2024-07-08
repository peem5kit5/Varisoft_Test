using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Health_UI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Health hp;

    public void Init(Health _health)
    {
        hp = _health;

        slider.maxValue = _health.MaxHP;
        slider.SetValueWithoutNotify(_health.HP);

        hp.OnHpChange += OnHPValueChange;
    }

    private void OnHPValueChange(int _value)
    {
        slider.SetValueWithoutNotify(hp.HP);
    }
}
