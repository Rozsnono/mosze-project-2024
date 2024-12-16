using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryDisplay : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public Button acceptButton; // Gombok a történet elfogadásához vagy elutasításához
    public Button declineButton;

    public string fullStory = "A 22. század végére az emberiség eljutott az univerzum legtávolabbi pontjaira, és békés felfedezésre törekedett. Egy Alfa Centauri melletti csillagrendszerből érkező energiajelzés azonban mindent megváltoztatott. Az Intergalaktikus Flotta expedíciót indított az Arcadia nevű hajóval, hogy felfedje a jel eredetét. A legénység furcsa téridő-hatásokkal szembesült, majd rátalált egy ősi szerkezetre, az Obszervátorra, amely kaput jelentett más világok felé. Az Obszervátor védelmezői, az Őrzők, felébredtek, és az emberek kénytelenek voltak szembeszállni velük. Miközben az Arcadia legénysége az energiaforrást tanulmányozta, döntést kellett hozniuk: visszavigyék a Földre, vagy megsemmisítsék. Bár az energiaforrást megszerezték, az Őrzők fenyegetése továbbra is az emberiség felett lebeg. A galaxis jövője most bizonytalan."; // Itt add meg a teljes szöveget
    public float textSpeed = 0.05f; // A szöveg karakterenkénti megjelenítésének sebessége

    void Start()
    {
        acceptButton.gameObject.SetActive(false); // A gombok kezdetben el vannak rejtve
        declineButton.gameObject.SetActive(false);
        StartCoroutine(DisplayStory());

        acceptButton.onClick.AddListener(Accept);
        declineButton.onClick.AddListener(Decline);
    }

    IEnumerator DisplayStory()
    {
        storyText.text = ""; // Törli a szövegmező tartalmát az elején
        foreach (char letter in fullStory.ToCharArray())
        {
            storyText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        // A történet végén megjelennek a gombok
        acceptButton.gameObject.SetActive(true);
        declineButton.gameObject.SetActive(true);
    }

    void Accept()
    {
        // Az elfogadás esetén betölt egy másik jelenetet (a játék következő része)
        SceneManager.LoadScene("GarageMenu"); // Csereld le a következő jelenet nevére
    }

    void Decline()
    {
        // Az elutasítás esetén visszatér a főmenübe
        SceneManager.LoadScene("MainMenu");
    }
}
