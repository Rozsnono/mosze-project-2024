using NUnit.Framework;
using System;

public class GameManager
{
    public int gridSize = 100;
    public int[] enemyCount = new int[] { 10, 10 };
    public int[] asteroidCount = new int[] { 50, 10 };

    public GameManager(int gridSize)
    {
        this.gridSize = gridSize;
    }

    public Tuple<float, float> GetRandomPosition()
    {
        float x, y;
        Random rand = new Random();
        do
        {
            x = (float)(rand.NextDouble() * (2 * gridSize) - gridSize);
            y = (float)(rand.NextDouble() * (2 * gridSize) - gridSize);
        } while (x < 20 && x > -20 || y < 20 && y > -20);

        return new Tuple<float, float>(x, y);
    }

    public Tuple<float, float> GetRandomBossPosition()
    {
        Random rand = new Random();
        float x = rand.Next(0, 4);
        switch (x)
        {
            case 0:
                return new Tuple<float, float>(gridSize - 20, gridSize - 20);
            case 1:
                return new Tuple<float, float>(-gridSize + 20, gridSize - 20);
            case 2:
                return new Tuple<float, float>(gridSize - 20, -gridSize + 20);
            case 3:
                return new Tuple<float, float>(-gridSize + 20, -gridSize + 20);
            default:
                return new Tuple<float, float>(gridSize - 20, gridSize - 20);
        }
    }
}

public class GameManagerTests
{
    private GameManager _gameManager;

    [SetUp]
    public void SetUp()
    {
        // Inicializáljuk a GameManager-t
        _gameManager = new GameManager(100); // Például 100-as grid méret
    }

    [Test]
    public void Test_GetRandomPosition_GeneratesValidPosition()
    {
        // Véletlenszerû pozíció generálása
        var position = _gameManager.GetRandomPosition();

        // Ellenõrizzük, hogy a generált pozíció kívül esik-e a középpont körüli tartományon (-20, 20)
        Assert.IsTrue(Math.Abs(position.Item1) > 20, "X pozíció túl közel van a középponthoz.");
        Assert.IsTrue(Math.Abs(position.Item2) > 20, "Y pozíció túl közel van a középponthoz.");
    }

    [Test]
    public void Test_GetRandomBossPosition_ReturnsValidPosition()
    {
        // Véletlenszerû boss pozíció
        var position = _gameManager.GetRandomBossPosition();

        // Ellenõrizzük, hogy a boss pozíciók valamelyikének megfelelõ érték van
        bool isValidPosition = (position.Item1 == 80 && position.Item2 == 80) ||
                               (position.Item1 == -80 && position.Item2 == 80) ||
                               (position.Item1 == 80 && position.Item2 == -80) ||
                               (position.Item1 == -80 && position.Item2 == -80);

        Assert.IsTrue(isValidPosition, "Boss pozíció érvénytelen.");
    }
}
