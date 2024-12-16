using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverDisplay : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public Button acceptButton; // A "Tovább" gomb a játékos döntéséhez

    public string fullStory = "Game Over";
    public float textSpeed = 0.05f; // A szöveg megjelenítési sebessége (karakterenkénti késleltetés)

    void Start()
    {
        acceptButton.gameObject.SetActive(false); // A gomb kezdetben rejtve van
        StartCoroutine(DisplayStory()); // Elindítja a "Game Over" szöveg karakterenkénti megjelenítését

        acceptButton.onClick.AddListener(Accept); // A gombhoz esemény-hozzárendelés
    }

    IEnumerator DisplayStory()
    {
        storyText.text = ""; // Törli a szövegmezőt az elején
        foreach (char letter in fullStory.ToCharArray()) // Karakterenként megjeleníti a teljes szöveget
        {
            storyText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // A történet végén megjelenik a gomb
        acceptButton.gameObject.SetActive(true);
    }

    void Accept()
    {
        // Átvált a főmenü jelenetére
        SceneManager.LoadScene("MainMenu");
    }
}
