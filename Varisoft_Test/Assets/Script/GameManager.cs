using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (WorldGenerator))]
[RequireComponent(typeof (FramerateOptimizer))]
public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    [SerializeField] private GameObject entityBaseObject;
    [SerializeField] private GameObject tileBaseObject;

    [Header("Dependencies")]
    [SerializeField] private WorldGenerator worldGen;
    [SerializeField] private FramerateOptimizer framerateOptimizer;
    [SerializeField] private Player player;
    [SerializeField] private Health health;

    public override void Awake()
    {
        framerateOptimizer.Init();

        worldGen = GetComponent<WorldGenerator>();
        worldGen.Init();

        InitializeWorldGenerator();
        InitializePlayer();
    }

    private void Start() 
    {
        
    } 

    private void InitializeWorldGenerator()
    {
        worldGen.GenerateTile();
        worldGen.GenerateObstacle();
        worldGen.GenerateEntity();
    }

    private void InitializePlayer()
    {
        player.Init();
    }

    public void GameOver()
    {

    }
}
