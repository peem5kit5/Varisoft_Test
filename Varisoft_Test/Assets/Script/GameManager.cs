using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (WorldGenerator))]
[RequireComponent(typeof (FramerateOptimizer))]
public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject entityBaseObject;
    [SerializeField] private GameObject tileBaseObject;
    [SerializeField] private GameObject particleObject;

    [Header("Dependencies")]
    [SerializeField] private WorldGenerator worldGen;
    [SerializeField] private FramerateOptimizer framerateOptimizer;
    [SerializeField] private Player player;
    [SerializeField] private Health health;

    private void Awake()
    {
        worldGen = GetComponent<WorldGenerator>();

        InitializeWorldGenerator();
        InitializePlayer();
    }

    private void Start() 
    {
        
    } 

    private void InitializeWorldGenerator()
    {
        worldGen.GenerateTile();
        worldGen.GenerateEntity();
    }

    private void InitializePlayer()
    {
        framerateOptimizer.Init();
        player.Init();
    }

    public GameObject GetParticle()
    {
        return particleObject;
    }
}
