using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisiton : MonoBehaviour
{
    private TextMeshProUGUI healthText;      // A UI Text elem, ahol az �leter�t megjelen�tj�k

    private void Start()
    {

        GameObject hTt = GameObject.FindGameObjectWithTag("HealtText");
        healthText = hTt.GetComponent<TextMeshProUGUI>();
        healthText.text = $"{PlayerStats.Instance.playerHealth:F0}/{PlayerStats.Instance.maxHealth:F0}";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerStats.Instance.playerHealth -= 10;
            Debug.Log("J�t�kos �tk�z�tt egy ellens�ggel! �letek: " + PlayerStats.Instance.playerHealth);
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            PlayerStats.Instance.playerHealth -= 5;
            Debug.Log("J�t�kos �tk�z�tt egy aszteroid�val! �letek: " + PlayerStats.Instance.playerHealth);
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            PlayerStats.Instance.playerHealth -= 5;
            Destroy(collision.gameObject);
            Debug.Log("J�t�kos �tk�z�tt egy rak�t�val! �letek: " + PlayerStats.Instance.playerHealth);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            PlayerStats.Instance.playerHealth -= 50;
        }
        else if (collision.gameObject.CompareTag("Homing"))
        {
            PlayerStats.Instance.playerHealth -= 15;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Gate"))
        {
            PlayerStats.Instance.currectGame += 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game_level"+ PlayerStats.Instance.currectGame);
        }
        // Ha az �letek elfogynak, megh�vhatsz egy game over met�dust

        if (PlayerStats.Instance.playerHealth <= 0)
        {
            GameOver();
        }
        healthText.text = $"{PlayerStats.Instance.playerHealth:F0}/{PlayerStats.Instance.maxHealth:F0}";

        //PlayerStats.Instance.speed = 0;
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Itt megh�vhatod a Game Over men�t, vagy �jraind�thatod a j�t�kot.
        SceneManager.LoadScene("GameOver");

    }
}
