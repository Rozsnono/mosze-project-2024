using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GarageMenu : MonoBehaviour
{
    public Image spaceshipImage;
    public TextMeshProUGUI spaceshipDescription;
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
        ShowSpaceship();
    }

    public void PreviousSpaceship()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = spaceships.Length - 1;
        ShowSpaceship();
    }

    void ShowSpaceship()
    {
        spaceshipImage.sprite = spaceships[currentIndex].image;
        spaceshipDescription.text = spaceships[currentIndex].description;
    }

    public void SelectSpaceship()
    {
        PlayerPrefs.SetInt("SelectedSpaceshipIndex", currentIndex);
        PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene"); // Átváltás a betöltési képernyõre
    }
}
