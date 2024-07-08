using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Tilemap obstacleTileMap;
    [SerializeField] private RuleTile ruleTile;
    [SerializeField] private RuleTile[] obstacleRuleTiles;

    [Header("Map Setting")]
    [SerializeField] private int mapSizeX;
    [SerializeField] private int mapSizeY;
    [SerializeField] private float tileSpacingX = 1;
    [SerializeField] private float tileSpacingY = 0.5f;
    [SerializeField] private float obstacleDensity = 1f;

    [Header("Entity Setting")]
    [SerializeField] private GameObject baseEntity;
    [SerializeField] private int maxEnemiesCount;
    [SerializeField] private EntityData[] allEntitiesData;

    private Dictionary<string, EntityData> entitiesDict = new Dictionary<string, EntityData>();
    private List<Vector3Int> tilePositions = new List<Vector3Int>();
    private List<Vector3Int> passableTile = new List<Vector3Int>();

    public List<Vector3Int> SpawnablePlayerTile => passableTile;
    public Tilemap TileMap => tileMap;
    public int MaxEnemiesCount => maxEnemiesCount;

    public void Init()
    {
        for(int i = 0; i < allEntitiesData.Length; i++)
            entitiesDict.Add(allEntitiesData[i].Name, allEntitiesData[i]);
    }

    public void GenerateTile()
    {
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                float _xPosition = (i - j) * tileSpacingX;
                float _yPosition = (i + j) * tileSpacingX * tileSpacingY;
                Vector3 _tilePosition = new Vector3(_xPosition, _yPosition, 0);
                Vector3Int _cellPosition = tileMap.WorldToCell(_tilePosition);

                tileMap.SetTile(_cellPosition, ruleTile);
                float _perlineValue = Mathf.PerlinNoise(i * 0.1f, j * 0.1f);

                if (_perlineValue > 0.5f)
                    tileMap.SetTile(_cellPosition, ruleTile);

                tilePositions.Add(_cellPosition);
                passableTile.Add(_cellPosition);
            }
        }
    }

    public void GenerateObstacle()
    {
        System.Random _random = new System.Random();
        List<Vector3Int> _obstaclePositions = new List<Vector3Int>();

        foreach (var _tilePosition in tilePositions)
        {
            if (_random.NextDouble() < obstacleDensity)
                _obstaclePositions.Add(_tilePosition);
        }

        if (IsPathAvailable(_obstaclePositions))
        {
            foreach (var _position in _obstaclePositions)
            {
                int _randomObstacleTile = Random.Range(0, obstacleRuleTiles.Length);
                obstacleTileMap.SetTile(_position, obstacleRuleTiles[_randomObstacleTile]);
                passableTile.Remove(_position);
            }
        }
        else
            GenerateObstacle();

        CenterAndResizeAstarGrid();
    }

    private void CenterAndResizeAstarGrid()
    {
        Vector3 _centerPosition = new Vector3(0, mapSizeY / 4, 0);

        AstarPath.active.data.gridGraph.center = _centerPosition;
        AstarPath.active.Scan();
    }

    public void GenerateEntity()
    {
        for(int i = 0; i < maxEnemiesCount; i++)
        {
            int _randomChance = Random.Range(0, allEntitiesData.Length);
            GameObject _entityBase = Instantiate(baseEntity, transform.position, Quaternion.identity);

            switch (_randomChance)
            {
                case 0:
                    var _melee = _entityBase.AddComponent<MeleeEntity>();
                    _melee.Init(entitiesDict["Warrior"]);
                    break;
                case 1:
                    var _ranged = _entityBase.AddComponent<RangedEntity>();
                    _ranged.Init(entitiesDict["Archer"]);
                    break;
                case 2:
                    var _bomber = _entityBase.AddComponent<BomberEntity>();
                    _bomber.Init(entitiesDict["Bomber"]);
                    break;
            }

            int _randomPosition = Random.Range(0, passableTile.Count);
            Vector3 _worldPos = tileMap.CellToWorld(passableTile[_randomPosition]);
            _entityBase.transform.position = _worldPos;

            passableTile.Remove(passableTile[_randomPosition]);
        }
    }


    private bool IsPathAvailable(List<Vector3Int> _obstaclePositions)
    {
        HashSet<Vector3Int> _obstacles = new HashSet<Vector3Int>(_obstaclePositions);
        Queue<Vector3Int> _queue = new Queue<Vector3Int>();
        HashSet<Vector3Int> _visited = new HashSet<Vector3Int>();

        Vector3Int _start = tileMap.WorldToCell(new Vector3(0, 0, 0));
        Vector3Int _end = tileMap.WorldToCell(new Vector3((mapSizeX - 1 - (mapSizeY - 1)) * tileSpacingX, (mapSizeX - 1 + mapSizeY - 1) * tileSpacingX * tileSpacingY, 0));

        _queue.Enqueue(_start);
        _visited.Add(_start);

        Vector3Int[] _directions = {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 1, 0),
            new Vector3Int(0, -1, 0)
        };

        while (_queue.Count > 0)
        {
            Vector3Int _current = _queue.Dequeue();

            if (_current == _end)
                return true;

            foreach (var _direction in _directions)
            {
                Vector3Int _neighbor = _current + _direction;

                if (!_visited.Contains(_neighbor) && !_obstacles.Contains(_neighbor) && tileMap.GetTile(_neighbor) != null)
                {
                    _queue.Enqueue(_neighbor);
                    _visited.Add(_neighbor);
                }
            }
        }

        return false;
    }
}

