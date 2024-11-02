using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;                    // Ellenség alap sebessége
    public int health = 10;                          // Életerõ
    public float detectionRange = 50f;              // Észlelési távolság
    public float attackRange = 10f;                 // Támadási távolság
    public float escapeDuration = 10f;              // Menekülési idõ másodpercben
    public GameObject rocketPrefab;                 // Rakéta prefab
    public Transform firePoint;                     // Rakéta indulási pontja
    public float rocketSpeed = 5f;                  // Rakéta sebessége
    private Transform player;                       // Játékos pozíciója

    private bool isEscaping = false;                // Menekülési állapot
    private float escapeStartTime;                  // Menekülési kezdési idõ

    public GameObject skillPointPrefab;             // Skill prefab

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Ellenõrzés, hogy a játékos észlelési tartományban van-e
        if (distanceToPlayer <= detectionRange && !isEscaping)
        {
            ApproachPlayer();
        }

        // Támadási tartományon belüli ellenõrzés
        if (distanceToPlayer <= attackRange && !isEscaping)
        {
            StartCoroutine(FireAndEscape());
        }

        // Ha menekülési mód aktív, akkor az ellenség távolodik a játékostól
        if (isEscaping)
        {
            EscapeFromPlayer();
            // Ha 30 másodperc eltelik, vagy a játékos 50 egységen kívül kerül, menekülés leállítása
            if (Time.time - escapeStartTime > escapeDuration || distanceToPlayer > detectionRange)
            {
                isEscaping = false;
            }
        }

        // Az ellenség mindig a játékos felé néz
        LookAtPlayer();
    }

    private void ApproachPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private IEnumerator FireAndEscape()
    {
        // Rakéta kilövése
        FireRocket();

        // Menekülési mód bekapcsolása
        isEscaping = true;
        escapeStartTime = Time.time;

        // Rövid szünet a rakéta kilövése után
        yield return new WaitForSeconds(1f);
    }

    private void FireRocket()
    {
        if (rocketPrefab != null && firePoint != null)
        {
            GameObject rocket = Instantiate(rocketPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (player.position - firePoint.position).normalized;
                rb.velocity = direction * rocketSpeed;
            }
        }
    }

    private void EscapeFromPlayer()
    {
        // Távolodás a játékostól
        Vector2 direction = (transform.position - player.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * 3 * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(skillPointPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(5);
            Destroy(collision.gameObject);

        }
        else if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Shield"))
        {
            TakeDamage(10);
        }
        else
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
