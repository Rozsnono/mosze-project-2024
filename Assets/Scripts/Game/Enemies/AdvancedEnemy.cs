using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;                    // Ellens�g alap sebess�ge
    public int health = 10;                          // �leter�
    public float detectionRange = 50f;              // �szlel�si t�vols�g
    public float attackRange = 10f;                 // T�mad�si t�vols�g
    public float escapeDuration = 10f;              // Menek�l�si id� m�sodpercben
    public GameObject rocketPrefab;                 // Rak�ta prefab
    public Transform firePoint;                     // Rak�ta indul�si pontja
    public float rocketSpeed = 5f;                  // Rak�ta sebess�ge
    private Transform player;                       // J�t�kos poz�ci�ja

    private bool isEscaping = false;                // Menek�l�si �llapot
    private float escapeStartTime;                  // Menek�l�si kezd�si id�

    public GameObject skillPointPrefab;             // Skill prefab

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Ellen�rz�s, hogy a j�t�kos �szlel�si tartom�nyban van-e
        if (distanceToPlayer <= detectionRange && !isEscaping)
        {
            ApproachPlayer();
        }

        // T�mad�si tartom�nyon bel�li ellen�rz�s
        if (distanceToPlayer <= attackRange && !isEscaping)
        {
            StartCoroutine(FireAndEscape());
        }

        // Ha menek�l�si m�d akt�v, akkor az ellens�g t�volodik a j�t�kost�l
        if (isEscaping)
        {
            EscapeFromPlayer();
            // Ha 30 m�sodperc eltelik, vagy a j�t�kos 50 egys�gen k�v�l ker�l, menek�l�s le�ll�t�sa
            if (Time.time - escapeStartTime > escapeDuration || distanceToPlayer > detectionRange)
            {
                isEscaping = false;
            }
        }

        // Az ellens�g mindig a j�t�kos fel� n�z
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
        // Rak�ta kil�v�se
        FireRocket();

        // Menek�l�si m�d bekapcsol�sa
        isEscaping = true;
        escapeStartTime = Time.time;

        // R�vid sz�net a rak�ta kil�v�se ut�n
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
        // T�volod�s a j�t�kost�l
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
