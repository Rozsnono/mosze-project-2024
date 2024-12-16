using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public string sceneToLoad; // A betölteni kívánt jelenet neve
    public TextMeshProUGUI loadingText; // Betöltési szöveg
    public Slider loadingSlider; // Betöltési csúszka

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        Debug.Log(sceneToLoad);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false; // Nem aktiválja automatikusan a jelenetet

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // A progress értéke 0-0.9 között van
            loadingSlider.value = progress;

            loadingText.text = "Toltes... " + (progress * 100).ToString("F0") + "%"; // Frissítjük a szöveget

            if (operation.progress >= 0.9f) // Ha a betöltés kész
            {
                loadingText.text = "Press 'SPACE' to continue";
                loadingSlider.gameObject.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space)) // Ha a játékos megnyomja a Space gombot
                {
                    operation.allowSceneActivation = true; // Betölti a jelenetet
                }
            }

            yield return null; // Várunk a következö frame-re
        }
    }
}
