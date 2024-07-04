using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("Map Setting")]
    [SerializeField] private int mapSize;

    [Header("Entity Setting")]
    [SerializeField] private int maxEnemiesCount;
    [SerializeField] private EntityData[] allEntitiesData;

    public void GenerateTile()
    {

    }

    public void GenerateEntity()
    {
        for(int i = 0; i < maxEnemiesCount; i++)
        {

        }
    }
}
