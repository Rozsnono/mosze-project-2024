using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldFollow : MonoBehaviour
{
    public Transform player;         // J�t�kos referenci�ja
    public Vector3 offset;           // Offset a pajzs �s a j�t�kos k�z�tt
    private float currentShieldHealth; // Pajzs aktu�lis �letereje

    private SpriteRenderer spriteRenderer;
    public TextMeshProUGUI shieldHealtText;      // A UI Text elem, ahol az id�t megjelen�tj�k
    private Collider2D shieldCollider; // Pajzs hitbox (Collider)

    private void Start()
    {
        // A j�t�kos referenci�j�nak ellen�rz�se
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        shieldCollider = GetComponent<Collider2D>();

        currentShieldHealth = PlayerStats.Instance.maxShieldHealth; // �leter� be�ll�t�sa maxim�lis �rt�kre
        DeactivateShield(); // Alap�rtelmezett �llapotban elrejtj�k a pajzsot
    }

    private void Update()
    {
        // Pajzs poz�ci�j�nak friss�t�se
        if (player != null)
        {
            transform.position = player.position + offset;
        }

        // Pajzs megjelen�t�se kattint�sra
        if (Input.GetMouseButtonDown(1)) // Jobb eg�rgomb kattint�s
        {
            ActivateShield();
        }
    }

    private void ActivateShield()
    {
        shieldHealtText.text = $"{currentShieldHealth:F0}/{PlayerStats.Instance.maxShieldHealth:F0}";
        currentShieldHealth = PlayerStats.Instance.maxShieldHealth; // Vissza�ll�tjuk az �leter�t
        spriteRenderer.enabled = true;         // Pajzs megjelen�t�se
        shieldCollider.enabled = true;         // Collider aktiv�l�sa
    }

    private void DeactivateShield()
    {
        spriteRenderer.enabled = false;        // Pajzs elrejt�se
        shieldCollider.enabled = false;        // Collider deaktiv�l�sa
    }

    public void TakeDamage(int damage)
    {
        // Sebz�s �rv�nyes�t�se a pajzson
        currentShieldHealth -= damage;
        shieldHealtText.text = $"{currentShieldHealth:F0}/{PlayerStats.Instance.maxShieldHealth:F0}";

        if (currentShieldHealth <= 0)
        {
            currentShieldHealth = 0;
            DeactivateShield(); // Pajzs kikapcsol�sa, ha �leter� null�ra cs�kken
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Csak akkor sebz�dik, ha a pajzs akt�v
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20); // Sebz�s �rt�k�nek megad�sa; p�ld�ul 20
            Destroy(collision.gameObject); // L�ved�k megsemmis�t�se
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(10); // Sebz�s �rt�k�nek megad�sa; p�ld�ul 20
            Destroy(collision.gameObject); // L�ved�k megsemmis�t�se
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(10); // Sebz�s �rt�k�nek megad�sa; p�ld�ul 20
            Destroy(collision.gameObject); // L�ved�k megsemmis�t�se
        }
    }
}
