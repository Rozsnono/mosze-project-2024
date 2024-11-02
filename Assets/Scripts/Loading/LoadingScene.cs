using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public string sceneToLoad; // A bet�lteni k�v�nt jelenet neve
    public TextMeshProUGUI loadingText; // Bet�lt�si sz�veg
    public Slider loadingSlider; // Bet�lt�si cs�szka

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; // Nem aktiv�lja automatikusan a jelenetet

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // A progress �rt�ke 0-0.9 k�z�tt van
            loadingSlider.value = progress;

            loadingText.text = "T�lt�s... " + (progress * 100).ToString("F0") + "%"; // Friss�tj�k a sz�veget

            if (operation.progress >= 0.9f) // Ha a bet�lt�s k�sz
            {
                loadingText.text = "Press 'SPACE' to continue"; // V�ltoztatjuk a sz�veget
                loadingSlider.gameObject.SetActive(false); // Opci�: elt�ntethetj�k a cs�szk�t

                if (Input.GetKeyDown(KeyCode.Space)) // Ha a j�t�kos megnyomja a Space gombot
                {
                    operation.allowSceneActivation = true; // Bet�lti a jelenetet
                }
            }

            yield return null; // V�runk a k�vetkez� frame-re
        }
    }
}
