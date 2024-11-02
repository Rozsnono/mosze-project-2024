using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        //Bet�lti a k�vetkez� Scene-t
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        //Kil�ptet a j�t�kb�l
        Application.Quit();
    }
}
