using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCollisiton : MonoBehaviour
{

    public Player player;
    private float currentHealth;

    public TextMeshProUGUI healthText;      // A UI Text elem, ahol az életerõt megjelenítjük


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

            Debug.Log("Játékos ütközött egy ellenséggel! Életek: " + currentHealth);


        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            currentHealth -= 5;

            Debug.Log("Játékos ütközött egy aszteroidával! Életek: " + currentHealth);

           
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            currentHealth -= 10;
            Destroy(collision.gameObject);
            Debug.Log("Játékos ütközött egy rakétával! Életek: " + currentHealth);

        }
        // Ha az életek elfogynak, meghívhatsz egy game over metódust

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
        // Itt meghívhatod a Game Over menüt, vagy újraindíthatod a játékot.
    }
}
