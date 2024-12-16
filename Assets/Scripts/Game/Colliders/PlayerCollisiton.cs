using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollisiton : MonoBehaviour
{
    private Slider healthBar;

    private void Start()
    {
        GameObject hTt = GameObject.FindGameObjectWithTag("HealthText");
        healthBar = hTt.GetComponent<Slider>();
        UIRefresh();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Ellenőrizzük, hogy a játékos egy ellenséggel ütközött-e
        {
            PlayerStats.Instance.playerHealth -= 10;
            Debug.Log("J�t�kos �tk�z�tt egy ellens�ggel! �letek: " + PlayerStats.Instance.playerHealth);
        }
        else if (collision.gameObject.CompareTag("Asteroid")) // Ellenőrizzük, hogy a játékos egy aszteroidával ütközött-e
        {
            PlayerStats.Instance.playerHealth -= 5;
            Debug.Log("J�t�kos �tk�z�tt egy aszteroid�val! �letek: " + PlayerStats.Instance.playerHealth);
        }
        else if (collision.gameObject.CompareTag("EnemyBullet")) // Ellenőrizzük, hogy a játékos egy ellenség rakétájával ütközött-e
        {
            PlayerStats.Instance.playerHealth -= 5;
            Destroy(collision.gameObject); // Rakéta eltávolítása a játéktérről
            Debug.Log("J�t�kos �tk�z�tt egy rak�t�val! �letek: " + PlayerStats.Instance.playerHealth);
        }
        else if (collision.gameObject.CompareTag("Boss")) // Ellenőrizzük, hogy a játékos a Boss-szal ütközött-e
        {
            PlayerStats.Instance.playerHealth -= 50;
        }
        else if (collision.gameObject.CompareTag("Homing")) // Ellenőrizzük, hogy a játékos egy hőkereső rakétával ütközött-e
        {
            PlayerStats.Instance.playerHealth -= 15;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Gate")) // Ellenőrizzük, hogy a játékos egy kapun áthaladt-e
        {
            PlayerStats.Instance.currectGame += 1;
            if(PlayerStats.Instance.currectGame < 4)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game_level"+ PlayerStats.Instance.currectGame);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Game_End");
            }
        }
        // Ha az �letek elfogynak, megh�vhatsz egy game over met�dust

        if (PlayerStats.Instance.playerHealth <= 0)
        {
            GameOver();
        }
        UIRefresh();


        //PlayerStats.Instance.speed = 0;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Itt megh�vhatod a Game Over men�t, vagy �jraind�thatod a j�t�kot.
        SceneManager.LoadScene("GameOver");

    }

    private void UIRefresh()
    {
        healthBar.gameObject.SetActive(true);
        healthBar.value = PlayerStats.Instance.playerHealth;
    }
}
