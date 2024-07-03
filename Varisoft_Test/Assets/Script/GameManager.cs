using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (WorldGenerator))]
public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject entityBaseObject;
    [SerializeField] private GameObject tileBaseObject;
    [SerializeField] private WorldGenerator worldGen;

    private void Awake()
    {
        worldGen = GetComponent<WorldGenerator>();
    }

    private void Start()
    {
        
    }
}
