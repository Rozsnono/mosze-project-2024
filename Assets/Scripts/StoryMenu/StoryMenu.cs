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

    public string fullStory = "Ez itt a történet kezdete. ..."; // Itt add meg a teljes szöveget
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

        // Ha a történet végére ért, megjelennek a gombok
        acceptButton.gameObject.SetActive(true);
        declineButton.gameObject.SetActive(true);
    }

    void Accept()
    {
        // Töltsük be a játék következõ jelenetét vagy kezdjünk el valamilyen logikát
        SceneManager.LoadScene("GarageMenu"); // Cseréld le a következõ jelenet nevére
    }

    void Decline()
    {
        // Vissza a fõmenübe, ha nem fogadja el a történetet
        SceneManager.LoadScene("MainMenu");
    }
}
