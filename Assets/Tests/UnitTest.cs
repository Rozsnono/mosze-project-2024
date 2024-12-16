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
        // Inicializ�ljuk a GameManager-t
        _gameManager = new GameManager(100); // P�ld�ul 100-as grid m�ret
    }

    [Test]
    public void Test_GetRandomPosition_GeneratesValidPosition()
    {
        // V�letlenszer� poz�ci� gener�l�sa
        var position = _gameManager.GetRandomPosition();

        // Ellen�rizz�k, hogy a gener�lt poz�ci� k�v�l esik-e a k�z�ppont k�r�li tartom�nyon (-20, 20)
        Assert.IsTrue(Math.Abs(position.Item1) > 20, "X poz�ci� t�l k�zel van a k�z�pponthoz.");
        Assert.IsTrue(Math.Abs(position.Item2) > 20, "Y poz�ci� t�l k�zel van a k�z�pponthoz.");
    }

    [Test]
    public void Test_GetRandomBossPosition_ReturnsValidPosition()
    {
        // V�letlenszer� boss poz�ci�
        var position = _gameManager.GetRandomBossPosition();

        // Ellen�rizz�k, hogy a boss poz�ci�k valamelyik�nek megfelel� �rt�k van
        bool isValidPosition = (position.Item1 == 80 && position.Item2 == 80) ||
                               (position.Item1 == -80 && position.Item2 == 80) ||
                               (position.Item1 == 80 && position.Item2 == -80) ||
                               (position.Item1 == -80 && position.Item2 == -80);

        Assert.IsTrue(isValidPosition, "Boss poz�ci� �rv�nytelen.");
    }
}
