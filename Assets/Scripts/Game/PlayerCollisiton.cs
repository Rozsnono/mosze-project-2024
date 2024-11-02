using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollisiton : MonoBehaviour
{

    public Player player;
    private float currentHealth;

    public TextMeshProUGUI healthText;      // A UI Text elem, ahol az �leter�t megjelen�tj�k


    private void Start()
    {
        currentHealth = PlayerStats.Instance.maxHealth;
        healthText.text = $"{currentHealth:F0}/{PlayerStats.Instance.maxHealth:F0}";

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= 10;

            Debug.Log("J�t�kos �tk�z�tt egy ellens�ggel! �letek: " + currentHealth);


        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            currentHealth -= 5;

            Debug.Log("J�t�kos �tk�z�tt egy aszteroid�val! �letek: " + currentHealth);

           
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            currentHealth -= 10;
            Destroy(collision.gameObject);
            Debug.Log("J�t�kos �tk�z�tt egy rak�t�val! �letek: " + currentHealth);

        }
        // Ha az �letek elfogynak, megh�vhatsz egy game over met�dust

        if (currentHealth <= 0)
        {
            GameOver();
        }
        healthText.text = $"{currentHealth:F0}/{PlayerStats.Instance.maxHealth:F0}";

        Destroy(collision.gameObject);
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Itt megh�vhatod a Game Over men�t, vagy �jraind�thatod a j�t�kot.
    }
}
