using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShieldFollow : MonoBehaviour
{
    private Transform player;         // Játékos referenciája
    public Vector3 offset;           // Offset a pajzs és a játékos között
    private float currentShieldHealth; // Pajzs aktuális életereje

    private SpriteRenderer spriteRenderer;
    private Collider2D shieldCollider; // Pajzs hitbox (Collider)

    public TextMeshProUGUI reloadTimerText;      // A UI Text elem, ahol az idõt megjelenítjük
    private Rigidbody2D rb;

    private float lastUse = 0f;


    public Image shieldHealtText;      // A UI Text elem, ahol az életerõt megjelenítjük
    public Sprite[] shieldImages;

    private void Start()
    {
        // A játékos referenciájának ellenõrzése
        if (player == null)
        {
            try
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch (System.Exception){}
        }
        rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        shieldCollider = GetComponent<Collider2D>();
        lastUse = Time.time - PlayerStats.Instance.shieldReloadTime;


        currentShieldHealth = PlayerStats.Instance.maxShieldHealth; // Életerõ beállítása maximális értékre
        DeactivateShield(); // Alapértelmezett állapotban elrejtjük a pajzsot
        UIReload();
    }

    private void Update()
    {
        // Pajzs pozíciójának frissítése
        FollowPlayer();
        if (player == null)
        {
            try
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            catch (System.Exception) { }
        }
        if (rb == null) 
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Pajzs megjelenítése kattintásra
        if (Input.GetMouseButtonDown(1) && Time.time >= lastUse + PlayerStats.Instance.shieldReloadTime) // Jobb egérgomb kattintás
        {
            ActivateShield();
        }

        UIReload();
        RotateTowardsMouse();
    }

    private void ActivateShield()
    {
        shieldHealtText.sprite = shieldImages[shieldImages.Length - 1];
        currentShieldHealth = PlayerStats.Instance.maxShieldHealth; // Visszaállítjuk az életerõt
        spriteRenderer.enabled = true;         // Pajzs megjelenítése
        shieldCollider.enabled = true;         // Collider aktiválása
    }

    private void DeactivateShield()
    {
        spriteRenderer.enabled = false;        // Pajzs elrejtése
        shieldCollider.enabled = false;        // Collider deaktiválása
        lastUse = Time.time;

    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }

    public void TakeDamage(int damage)
    {
        // Sebzés érvényesítése a pajzson
        currentShieldHealth -= damage;

        if (currentShieldHealth <= 0)
        {
            currentShieldHealth = 0;
            DeactivateShield(); // Pajzs kikapcsolása, ha életerõ nullára csökken
        }
        shieldHealtText.sprite = shieldImages[(int.Parse(currentShieldHealth.ToString()) / 5)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Csak akkor sebzõdik, ha a pajzs aktív
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20); // Sebzés érték;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(0); // Sebzés érték;
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(5); // Sebzés érték;
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(50); // Sebzés érték;
        }
        else if (collision.gameObject.CompareTag("Homing"))
        {
            TakeDamage(5); // Sebzés érték;
        }
    }

    private void UIReload()
    {
        // Újratöltési idõ számítása és kijelzése
        float timeSinceLastShot = Time.time - lastUse;
        float timeLeft = Mathf.Max(PlayerStats.Instance.shieldReloadTime - timeSinceLastShot, 0);

        // Frissítés a UI Text elemben
        if (timeLeft > 0)
        {
            reloadTimerText.text = $"{timeLeft:F1}s";
        }
        else
        {
            reloadTimerText.text = "0,0!";
        }
    }

    private void RotateTowardsMouse()
    {
        // Az egér pozíciójának lekérése a világban
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Forgatás az egér irányába
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
