using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;                // Az ellens�g sebess�ge
    public int health = 3;                      // Az ellens�g �letereje
    public float detectionRange = 30f;          // �szlel�si t�vols�g
    public float firingRange = 10f;              // T�zel�si t�vols�g
    public GameObject projectilePrefab;         // L�ved�k prefab
    public Transform firePoint;                 // T�zel�si pont, ahonnan a l�ved�k indul

    private Transform player;                   // A j�t�kos poz�ci�j�nak t�rol�sa
    private bool playerDetected = false;        // Jelzi, ha a j�t�kost egyszer m�r �szlelte

    public GameObject skillPointPrefab;             // Skill prefab


    private void Start()
    {
        // J�t�kos keres�se, felt�telezz�k, hogy a j�t�kosnak "Player" tag-je van
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Ha a j�t�kos hat�t�vols�gon bel�l van, akkor �szlelt�k
            if (distanceToPlayer <= detectionRange)
            {
                playerDetected = true;
            }

            // Ha a j�t�kos egyszer m�r �szlelve lett, az ellens�g k�veti �t
            if (playerDetected)
            {
                MoveTowardsPlayer();
            }

            // Mindig n�zzen a j�t�kos fel�
            LookAtPlayer();

            if(distanceToPlayer <= firingRange) 
            {
                Suicide();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // Mozg�s a j�t�kos ir�ny�ba
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    private void Suicide()
    {
        // Mozg�s a j�t�kos ir�ny�ba
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * 5 * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        // Az ellens�g forgat�sa, hogy mindig a j�t�kos fel� n�zzen
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void TakeDamage(int damage, bool isSkill = false)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(isSkill);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(5, true);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(10);
        }
    }

    private void Die(bool isSkill = false)
    {
        if (isSkill) { 
            Instantiate(skillPointPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
