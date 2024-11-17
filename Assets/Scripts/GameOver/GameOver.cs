using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverDisplay : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public Button acceptButton;

    public string fullStory = "Game Over"; // Itt add meg a teljes szöveget
    public float textSpeed = 0.05f;

    void Start()
    {
        acceptButton.gameObject.SetActive(false);
        StartCoroutine(DisplayStory());

        acceptButton.onClick.AddListener(Accept);
    }

    IEnumerator DisplayStory()
    {
        storyText.text = "";
        foreach (char letter in fullStory.ToCharArray())
        {
            storyText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // Ha a történet végére ért, megjelennek a gombok
        acceptButton.gameObject.SetActive(true);
    }

    void Accept()
    {
        // Töltsük be a játék következõ jelenetét vagy kezdjünk el valamilyen logikát
        SceneManager.LoadScene("MainMenu");
    }
}
