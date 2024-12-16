using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        //Betölti a következő Scene-t
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        //Kiléptett a játékból
        Application.Quit();
    }
}
