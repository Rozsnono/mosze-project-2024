using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;                // Az ellenség sebessége
    public int health = 3;                      // Az ellenség életereje
    public float detectionRange = 30f;          // Észlelési távolság
    public float firingRange = 10f;              // Tüzelési távolság
    public GameObject projectilePrefab;         // Lövedék prefab
    public Transform firePoint;                 // Tüzelési pont, ahonnan a lövedék indul

    private Transform player;                   // A játékos pozíciójának tárolása
    private bool playerDetected = false;        // Jelzi, ha a játékost egyszer már észlelte

    public GameObject skillPointPrefab;             // Skill prefab


    private void Start()
    {
        // Játékos keresése, feltételezzük, hogy a játékosnak "Player" tag-je van
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Ha a játékos hatótávolságon belül van, akkor észleltük
            if (distanceToPlayer <= detectionRange)
            {
                playerDetected = true;
            }

            // Ha a játékos egyszer már észlelve lett, az ellenség követi õt
            if (playerDetected)
            {
                MoveTowardsPlayer();
            }

            // Mindig nézzen a játékos felé
            LookAtPlayer();

            if(distanceToPlayer <= firingRange) 
            {
                Suicide();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // Mozgás a játékos irányába
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    private void Suicide()
    {
        // Mozgás a játékos irányába
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * 5 * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        // Az ellenség forgatása, hogy mindig a játékos felé nézzen
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
