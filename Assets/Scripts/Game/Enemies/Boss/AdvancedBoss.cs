using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedBoss : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth;
    public float moveSpeed = 0.5f;                // A boss sebess�ge
    public float spawnDistance = 30f;          // A spawn t�vols�g
    public int[] enemySpawnCount = new int[] { 10, 10 };           // Spawnoland� enemyk sz�ma
    public float enemySpawnTime = 20f;         // Enemyk spawnol�sa
    public GameObject[] enemyPrefab;             // Az enemy prefab
    private Transform player;                   // J�t�kos referenci�ja
    public Slider healthBarSlider;             // HP cs�szka UI

    private bool enemiesSpawned = false;       // Az ellens�gek m�r spawnoltak-e
    private float lastTimeEnemySpawned = 0f;

    public GameObject gatePrefab;

    private int enemyIndex = 0;

    private void Start()
    {
        currentHealth = GameManager.Instance.maxBossHealth;
        maxHealth = GameManager.Instance.maxBossHealth;
        healthBarSlider.maxValue = GameManager.Instance.maxBossHealth;
        healthBarSlider.value = currentHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (GameManager.Instance.maxBossHealth != null)
        {
            maxHealth = GameManager.Instance.maxBossHealth;
        }
        UpdateHealthBar();
        MoveTowardsPlayer();
        CheckPlayerDistance();
        float timeLeft = Mathf.Max(enemySpawnTime - (Time.time - lastTimeEnemySpawned), 0);
        if (timeLeft == 0)
        {
            enemiesSpawned = false;
        }
    }

    private void UpdateHealthBar()
    {
        healthBarSlider.value = currentHealth;
    }

    private void MoveTowardsPlayer()
    {
        // Mozg�s a j�t�kos ir�ny�ba
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        LookAtPlayer();
    }
    private void LookAtPlayer()
    {
        // Az ellens�g forgat�sa, hogy mindig a j�t�kos fel� n�zzen
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 270f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void CheckPlayerDistance()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= spawnDistance && !enemiesSpawned)
        {
            SpawnEnemies(enemyIndex);
            enemiesSpawned = true;
            lastTimeEnemySpawned = Time.time;
        }
    }

    private void SpawnEnemies(int index)
    {
        for (int i = 0; i < enemySpawnCount[index]; i++)
        {
            Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * 5f; // Spawn pont a boss k�zel�ben
            Instantiate(enemyPrefab[index], spawnPosition, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Asteroid"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Boss damage");
        currentHealth -= damage;
        if (currentHealth < GameManager.Instance.maxBossHealth / 2)
        {
            enemyIndex = 1;
        }
        if (currentHealth < GameManager.Instance.maxBossHealth / 3)
        {
            enemyIndex = 2;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // A boss hal�l�nak kezel�se
        Instantiate(gatePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
