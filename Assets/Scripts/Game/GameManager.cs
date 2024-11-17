using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public GameObject[] playerPrefab; // A j�t�kos prefabjai
    public GameObject[] enemyPrefab; // Az ellens�g prefabjai
    public GameObject[] asteroidPrefab; // Az aszteroida prefabjai
    public GameObject bossPrefab; // A Boss prefabjai
    public int gridSize = 100; // A grid m�rete (100x100)
    public int[] enemyCount = new int[] { 10, 10 }; // Az ellens�gek sz�ma
    public int[] asteroidCount = new int[] { 50, 10 }; // Az aszteroid�k sz�ma
    public Tilemap tilemap;
    public TileBase[] spaceTiles;

    public int maxBossHealth = 1000;

    public static GameManager Instance { get; private set; }


    private void Start()
    {
        SpawnPlayer(1);
        SpawnEnemies();
        SpawnAsteroids();
        GenerateTilemap();
        SpawnBoss();
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

    private void SpawnBoss()
    {
        Vector3 spawnPosition = GetRandomBossPosition();
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        float x, y;
        do
        {
            x = Random.Range(-gridSize, gridSize);
            y = Random.Range(-gridSize, gridSize);
        } while (x < 20 && x > -20 || y < 20 && y > -20);

        return new Vector3(x, y, -1);
    }

    private Vector3 GetRandomBossPosition()
    {
        float x = Random.Range(0, 3);
        switch (x)
        {
            case 0:
                return new Vector3(gridSize - 20, gridSize - 20, -1);
            case 1:
                return new Vector3(-gridSize + 20, gridSize - 20, -1);
            case 2:
                return new Vector3(gridSize - 20, -gridSize + 20, -1);
            case 3:
                return new Vector3(-gridSize + 20, -gridSize + 20, -1);
            default: return new Vector3(gridSize - 20, gridSize - 20, -1);
        }
    }

    private void GenerateTilemap()
    {
        for (int x = -gridSize; x < gridSize; x++)
        {
            for (int y = -gridSize; y < gridSize; y++)
            {
                TileBase selectedTile = GetWeightedRandomTile();
                tilemap.SetTile(new Vector3Int(x, y, 0), selectedTile);
            }
        }
    }


    private void SpawnPlayer(int index)
    {
        Vector3 spawnPosition = new Vector3(0,0,0);
        Instantiate(playerPrefab[index], spawnPosition, Quaternion.identity);
    }


    private TileBase GetWeightedRandomTile()
    {
        // Ha nincs csempe, t�rjen vissza null�val
        if (spaceTiles == null || spaceTiles.Length == 0)
            return null;

        // S�lyozott v�letlen v�laszt�s
        int totalWeight = 100;
        for (int i = 0; i < spaceTiles.Length; i++)
        {
            totalWeight += spaceTiles.Length - i; // Az els� csemp�nek nagyobb s�lyt adunk
        }

        int randomWeight = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for (int i = 0; i < spaceTiles.Length; i++)
        {
            cumulativeWeight += (i == 0) ? 100 : 1;
            if (randomWeight < cumulativeWeight)
            {
                return spaceTiles[i];
            }
        }

        // Biztons�gi int�zked�sk�nt az els� elemet adja vissza, ha nincs tal�lat
        return spaceTiles[0];
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Az objektum nem ker�l megsemmis�t�sre jelenetv�lt�skor
        }
        else
        {
            Destroy(gameObject); // Ha m�r l�tezik egy p�ld�ny, t�r�lj�k a m�sodikat
        }
    }
}
