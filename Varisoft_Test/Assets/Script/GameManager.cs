using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (WorldGenerator))]
[RequireComponent(typeof (FramerateOptimizer))]
public class GameManager : Singleton<GameManager>
{
    [Header("Dependencies")]
    [SerializeField] private WorldGenerator worldGen;
    [SerializeField] private FramerateOptimizer framerateOptimizer;
    [SerializeField] private UI_Controller uiController;
    [SerializeField] private Player player;
    [SerializeField] private Health health;

    public bool IsGamePlay;

    [Header("Overalll References")]
    public GameObject DeathParticle;
    public Player Player => player;

    public int MaxEnemies => worldGen.MaxEnemiesCount;
    
    public override void Awake()
    {
        IsGamePlay = true;

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

        int _randomPosition = Random.Range(0, worldGen.SpawnablePlayerTile.Count);
        Vector3 _position = worldGen.TileMap.CellToWorld(worldGen.SpawnablePlayerTile[_randomPosition]);

        player.transform.position = _position;

        uiController.Init(player);
    }

    public void GameOver()
    {
        IsGamePlay = false;
        uiController.Retry();
    }

    public void EntityDeath() => uiController.CheckRemaining();
}
