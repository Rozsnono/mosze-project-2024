using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameManager
{
    //[Test]
    //public void SpawnPlayer_ShouldInstantiateCorrectPrefab()
    //{
    //    // Arrange
    //    var gameManager = new GameObject().AddComponent<GameManager>();
    //    var playerPrefab = new GameObject("PlayerPrefab");
    //    gameManager.playerPrefab = new GameObject[] { playerPrefab };

    //    PlayerStats.Instance = new PlayerStats { playerShipIndex = 0 };

    //    // Act
    //    gameManager.SpawnPlayer(PlayerStats.Instance.playerShipIndex);

    //    // Assert
    //    var instantiatedPlayer = GameObject.Find("PlayerPrefab(Clone)");
    //    Assert.IsNotNull(instantiatedPlayer);
    //    Assert.AreEqual(Vector3.zero, instantiatedPlayer.transform.position);
    //}

    //[Test]
    //public void GetRandomPosition_ShouldReturnValidPosition()
    //{
    //    // Arrange
    //    var gameManager = new GameObject().AddComponent<GameManager>();
    //    gameManager.gridSize = 100;

    //    // Act
    //    Vector3 randomPosition = gameManager.GetRandomPosition();

    //    // Assert
    //    Assert.IsTrue(randomPosition.x >= -100 && randomPosition.x <= 100);
    //    Assert.IsTrue(randomPosition.y >= -100 && randomPosition.y <= 100);
    //    Assert.IsTrue(!(randomPosition.x > -20 && randomPosition.x < 20 && randomPosition.y > -20 && randomPosition.y < 20));
    //}

    //[Test]
    //public void SpawnEnemies_ShouldInstantiateCorrectNumberOfEnemies()
    //{
    //    // Arrange
    //    var gameManager = new GameObject().AddComponent<GameManager>();
    //    var enemyPrefab1 = new GameObject("Enemy1Prefab");
    //    var enemyPrefab2 = new GameObject("Enemy2Prefab");
    //    gameManager.enemyPrefab = new GameObject[] { enemyPrefab1, enemyPrefab2 };
    //    gameManager.enemyCount = new int[] { 5, 3 };

    //    // Act
    //    gameManager.SpawnEnemies();

    //    // Assert
    //    Assert.AreEqual(5, GameObject.FindObjectsOfType(enemyPrefab1.GetType()).Length);
    //    Assert.AreEqual(3, GameObject.FindObjectsOfType(enemyPrefab2.GetType()).Length);
    //}

    //[Test]
    //public void SpawnAsteroids_ShouldInstantiateCorrectNumberOfAsteroids()
    //{
    //    // Arrange
    //    var gameManager = new GameObject().AddComponent<GameManager>();
    //    var asteroidPrefab1 = new GameObject("Asteroid1Prefab");
    //    var asteroidPrefab2 = new GameObject("Asteroid2Prefab");
    //    gameManager.asteroidPrefab = new GameObject[] { asteroidPrefab1, asteroidPrefab2 };
    //    gameManager.asteroidCount = new int[] { 10, 5 };

    //    // Act
    //    gameManager.SpawnAsteroids();

    //    // Assert
    //    Assert.AreEqual(10, GameObject.FindObjectsOfType(asteroidPrefab1.GetType()).Length);
    //    Assert.AreEqual(5, GameObject.FindObjectsOfType(asteroidPrefab2.GetType()).Length);
    //}

    //[Test]
    //public void GenerateTilemap_ShouldFillTilemap()
    //{
    //    // Arrange
    //    var gameManager = new GameObject().AddComponent<GameManager>();
    //    var tilemap = new GameObject().AddComponent<Tilemap>();
    //    gameManager.tilemap = tilemap;
    //    gameManager.gridSize = 10;
    //    gameManager.spaceTiles = new TileBase[] { ScriptableObject.CreateInstance<TileBase>() };

    //    // Act
    //    gameManager.GenerateTilemap();

    //    // Assert
    //    for (int x = -10; x < 10; x++)
    //    {
    //        for (int y = -10; y < 10; y++)
    //        {
    //            Assert.IsNotNull(tilemap.GetTile(new Vector3Int(x, y, 0)));
    //        }
    //    }
    //}

}
