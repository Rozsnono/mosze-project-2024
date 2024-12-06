using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GarageMenu : MonoBehaviour
{
    public Image spaceshipImage; // A hajó képének megjelenítésére szolgáló UI elem
    public TextMeshProUGUI spaceshipDescription; // A hajó leírását megjelenítő szöveg UI elem
    public TextMeshProUGUI spaceShipName; // A hajó nevét megjelenítő szöveg UI elem
    public Spaceship[] spaceships;

    private int currentIndex = 0;

    void Start()
    {
        ShowSpaceship(); // Megjeleníti az aktuálisan kiválasztott űrhajót az induláskor
    }

    public void NextSpaceship() // Következő űrhajóra vált
    {
        currentIndex++;
        if (currentIndex >= spaceships.Length) currentIndex = 0;
        PlayerStats.Instance.ChangePlayerShip(currentIndex); // Frissíti a játékos aktuális hajóját
        ShowSpaceship();
    }

    public void PreviousSpaceship() // Előző űrhajóra vált
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = spaceships.Length - 1;
        PlayerStats.Instance.ChangePlayerShip(currentIndex);
        ShowSpaceship(); // Frissíti a megjelenített hajó adatait
    }

    void ShowSpaceship() // Megjeleníti az aktuálisan kiválasztott űrhajó adatait
    {
        spaceshipImage.sprite = spaceships[currentIndex].image; // Beállítja a hajó képét
        spaceShipName.text = spaceships[currentIndex].name; // Beállítja a hajó nevét
        spaceshipDescription.text = spaceships[currentIndex].description; // Beállítja a hajó leírását
    }

    public void SelectSpaceship() // Kiválasztja az aktuális űrhajót és betölti a játékot
    {
        PlayerPrefs.SetInt("SelectedSpaceshipIndex", currentIndex); // Elmenti a kiválasztott űrhajó indexét
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game"); // Átvált a játékmenethez tartozó jelenetre
    }
}
