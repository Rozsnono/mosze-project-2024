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

    public string fullStory = "Game Over"; // Itt add meg a teljes sz�veget
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

        // Ha a t�rt�net v�g�re �rt, megjelennek a gombok
        acceptButton.gameObject.SetActive(true);
    }

    void Accept()
    {
        // T�lts�k be a j�t�k k�vetkez� jelenet�t vagy kezdj�nk el valamilyen logik�t
        SceneManager.LoadScene("MainMenu");
    }
}
