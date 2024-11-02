using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryDisplay : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public Button acceptButton;
    public Button declineButton;

    public string fullStory = "Ez itt a t�rt�net kezdete. ..."; // Itt add meg a teljes sz�veget
    public float textSpeed = 0.05f;

    void Start()
    {
        acceptButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
        StartCoroutine(DisplayStory());

        acceptButton.onClick.AddListener(Accept);
        declineButton.onClick.AddListener(Decline);
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
        declineButton.gameObject.SetActive(true);
    }

    void Accept()
    {
        // T�lts�k be a j�t�k k�vetkez� jelenet�t vagy kezdj�nk el valamilyen logik�t
        SceneManager.LoadScene("GarageMenu"); // Cser�ld le a k�vetkez� jelenet nev�re
    }

    void Decline()
    {
        // Vissza a f�men�be, ha nem fogadja el a t�rt�netet
        SceneManager.LoadScene("MainMenu");
    }
}
