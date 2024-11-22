using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShieldFollow : MonoBehaviour
{
    private Transform player;         // J�t�kos referenci�ja
    public Vector3 offset;           // Offset a pajzs �s a j�t�kos k�z�tt
    private float currentShieldHealth; // Pajzs aktu�lis �letereje

    private SpriteRenderer spriteRenderer;
    private Collider2D shieldCollider; // Pajzs hitbox (Collider)

    public TextMeshProUGUI reloadTimerText;      // A UI Text elem, ahol az id�t megjelen�tj�k
    private Rigidbody2D rb;

    private float lastUse = 0f;


    public Image shieldHealtText;      // A UI Text elem, ahol az �leter�t megjelen�tj�k
    public Sprite[] shieldImages;

    private void Start()
    {
        // A j�t�kos referenci�j�nak ellen�rz�se
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


        currentShieldHealth = PlayerStats.Instance.maxShieldHealth; // �leter� be�ll�t�sa maxim�lis �rt�kre
        DeactivateShield(); // Alap�rtelmezett �llapotban elrejtj�k a pajzsot
        UIReload();
    }

    private void Update()
    {
        // Pajzs poz�ci�j�nak friss�t�se
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

        // Pajzs megjelen�t�se kattint�sra
        if (Input.GetMouseButtonDown(1) && Time.time >= lastUse + PlayerStats.Instance.shieldReloadTime) // Jobb eg�rgomb kattint�s
        {
            ActivateShield();
        }

        UIReload();
        RotateTowardsMouse();
    }

    private void ActivateShield()
    {
        shieldHealtText.sprite = shieldImages[shieldImages.Length - 1];
        currentShieldHealth = PlayerStats.Instance.maxShieldHealth; // Vissza�ll�tjuk az �leter�t
        spriteRenderer.enabled = true;         // Pajzs megjelen�t�se
        shieldCollider.enabled = true;         // Collider aktiv�l�sa
    }

    private void DeactivateShield()
    {
        spriteRenderer.enabled = false;        // Pajzs elrejt�se
        shieldCollider.enabled = false;        // Collider deaktiv�l�sa
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
        // Sebz�s �rv�nyes�t�se a pajzson
        currentShieldHealth -= damage;

        if (currentShieldHealth <= 0)
        {
            currentShieldHealth = 0;
            DeactivateShield(); // Pajzs kikapcsol�sa, ha �leter� null�ra cs�kken
        }
        shieldHealtText.sprite = shieldImages[(int.Parse(currentShieldHealth.ToString()) / 5)];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Csak akkor sebz�dik, ha a pajzs akt�v
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20); // Sebz�s �rt�k;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(0); // Sebz�s �rt�k;
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(5); // Sebz�s �rt�k;
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(50); // Sebz�s �rt�k;
        }
        else if (collision.gameObject.CompareTag("Homing"))
        {
            TakeDamage(5); // Sebz�s �rt�k;
        }
    }

    private void UIReload()
    {
        // �jrat�lt�si id� sz�m�t�sa �s kijelz�se
        float timeSinceLastShot = Time.time - lastUse;
        float timeLeft = Mathf.Max(PlayerStats.Instance.shieldReloadTime - timeSinceLastShot, 0);

        // Friss�t�s a UI Text elemben
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
        // Az eg�r poz�ci�j�nak lek�r�se a vil�gban
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Forgat�s az eg�r ir�ny�ba
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
