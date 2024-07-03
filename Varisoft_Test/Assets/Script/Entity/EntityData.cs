using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EntityData")]
public class EntityData : ScriptableObject
{
    public string Name;
    public float Speed;
    public int Damage;

    public Animator Anim;
    public Sprite Sprite;
}
