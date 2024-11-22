using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GarageMenu : MonoBehaviour
{
    public Image spaceshipImage;
    public TextMeshProUGUI spaceshipDescription;
    public TextMeshProUGUI spaceShipName;
    public Spaceship[] spaceships;

    private int currentIndex = 0;

    void Start()
    {
        ShowSpaceship();
    }

    public void NextSpaceship()
    {
        currentIndex++;
        if (currentIndex >= spaceships.Length) currentIndex = 0;
        PlayerStats.Instance.ChangePlayerShip(currentIndex);
        ShowSpaceship();
    }

    public void PreviousSpaceship()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = spaceships.Length - 1;
        PlayerStats.Instance.ChangePlayerShip(currentIndex);
        ShowSpaceship();
    }

    void ShowSpaceship()
    {
        spaceshipImage.sprite = spaceships[currentIndex].image;
        spaceShipName.text = spaceships[currentIndex].name;
        spaceshipDescription.text = spaceships[currentIndex].description;
    }

    public void SelectSpaceship()
    {
        PlayerPrefs.SetInt("SelectedSpaceshipIndex", currentIndex);
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game"); // Átváltás a betöltési képernyõre
    }
}
