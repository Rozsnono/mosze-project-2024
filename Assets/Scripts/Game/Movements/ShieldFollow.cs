using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldFollow : MonoBehaviour
{
    public Transform player;         // Játékos referenciája
    public Vector3 offset;           // Offset a pajzs és a játékos között
    private float currentShieldHealth; // Pajzs aktuális életereje

    private SpriteRenderer spriteRenderer;
    public TextMeshProUGUI shieldHealtText;      // A UI Text elem, ahol az idõt megjelenítjük
    private Collider2D shieldCollider; // Pajzs hitbox (Collider)

    private void Start()
    {
        // A játékos referenciájának ellenõrzése
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        shieldCollider = GetComponent<Collider2D>();

        currentShieldHealth = PlayerStats.Instance.maxShieldHealth; // Életerõ beállítása maximális értékre
        DeactivateShield(); // Alapértelmezett állapotban elrejtjük a pajzsot
    }

    private void Update()
    {
        // Pajzs pozíciójának frissítése
        if (player != null)
        {
            transform.position = player.position + offset;
        }

        // Pajzs megjelenítése kattintásra
        if (Input.GetMouseButtonDown(1)) // Jobb egérgomb kattintás
        {
            ActivateShield();
        }
    }

    private void ActivateShield()
    {
        shieldHealtText.text = $"{currentShieldHealth:F0}/{PlayerStats.Instance.maxShieldHealth:F0}";
        currentShieldHealth = PlayerStats.Instance.maxShieldHealth; // Visszaállítjuk az életerõt
        spriteRenderer.enabled = true;         // Pajzs megjelenítése
        shieldCollider.enabled = true;         // Collider aktiválása
    }

    private void DeactivateShield()
    {
        spriteRenderer.enabled = false;        // Pajzs elrejtése
        shieldCollider.enabled = false;        // Collider deaktiválása
    }

    public void TakeDamage(int damage)
    {
        // Sebzés érvényesítése a pajzson
        currentShieldHealth -= damage;
        shieldHealtText.text = $"{currentShieldHealth:F0}/{PlayerStats.Instance.maxShieldHealth:F0}";

        if (currentShieldHealth <= 0)
        {
            currentShieldHealth = 0;
            DeactivateShield(); // Pajzs kikapcsolása, ha életerõ nullára csökken
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Csak akkor sebzõdik, ha a pajzs aktív
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20); // Sebzés értékének megadása; például 20
            Destroy(collision.gameObject); // Lövedék megsemmisítése
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(10); // Sebzés értékének megadása; például 20
            Destroy(collision.gameObject); // Lövedék megsemmisítése
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(10); // Sebzés értékének megadása; például 20
            Destroy(collision.gameObject); // Lövedék megsemmisítése
        }
    }
}
