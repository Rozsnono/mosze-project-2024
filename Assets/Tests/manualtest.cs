using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int gridSize = 100; // A grid mérete
        int[] asteroidCount = { 50, 10 }; // Két típusú aszteroida példányszáma
        string[] asteroidPrefab = { "AsteroidType1", "AsteroidType2" }; // Aszteroida típusok nevei

        SpawnAsteroids(asteroidCount, asteroidPrefab, gridSize);
    }

    static void SpawnAsteroids(int[] asteroidCount, string[] asteroidPrefab, int gridSize)
    {
        List<string> spawnedAsteroids = new List<string>();

        for (int i = 0; i < asteroidCount.Length; i++)
        {
            for (int x = 0; x < asteroidCount[i]; x++)
            {
                string spawnPosition = GetRandomPosition(gridSize); // Véletlenszerű pozíció generálása
                spawnedAsteroids.Add($"{asteroidPrefab[i]} at {spawnPosition}");
            }
        }

        // Kiírás a konzolra a teszteléshez
        foreach (var asteroid in spawnedAsteroids)
        {
            Console.WriteLine(asteroid);
        }
    }

    static string GetRandomPosition(int gridSize)
    {
        Random rand = new Random();
        int x, y;

        // Véletlenszerű pozíció generálása, kizárva a középső 40x40-es területet
        do
        {
            x = rand.Next(-gridSize, gridSize);
            y = rand.Next(-gridSize, gridSize);
        } while (x < 20 && x > -20 && y < 20 && y > -20);

        int z = -1; // A Z-tengely értéke fix

        return $"({x}, {y}, {z})";
    }
}

//Unity specifikus elemek lecserélve
//A teszt az aszteroida véletlen spawnolást teszteli.
