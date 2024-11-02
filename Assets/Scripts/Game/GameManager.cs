using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefab; // Az ellenség prefabjai
    public GameObject[] asteroidPrefab; // Az aszteroida prefabjai
    public int gridSize = 100; // A grid mérete (100x100)
    public int[] enemyCount = new int[] { 10, 10 }; // Az ellenségek száma
    public int[] asteroidCount = new int[] { 50, 10 }; // Az aszteroidák száma
    public Player player;

    private void Start()
    {
        SpawnEnemies();
        SpawnAsteroids();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount.Length; i++) {
            for (int x = 0; x < enemyCount[i]; x++)
            {
                Vector3 spawnPosition = GetRandomPosition();
                Instantiate(enemyPrefab[i], spawnPosition, Quaternion.identity);
            }
        }
    }

    private void SpawnAsteroids()
    {
        for (int i = 0; i < asteroidCount.Length; i++)
        {
            for (int x = 0; x < asteroidCount[i]; x++)
            {
                Vector3 spawnPosition = GetRandomPosition();
                Instantiate(asteroidPrefab[i], spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-gridSize, gridSize);
        float y = Random.Range(-gridSize, gridSize);
        return new Vector3(x, y, -1);
    }
}
