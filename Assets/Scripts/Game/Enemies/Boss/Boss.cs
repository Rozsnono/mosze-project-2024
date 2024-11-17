using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth;
    public float spawnDistance = 30f;          // A spawn távolság
    public int[] enemySpawnCount = new int[] { 10, 10 };           // Spawnolandó enemyk száma
    public float enemySpawnTime = 30f;         // Enemyk spawnolása
    public GameObject[] enemyPrefab;             // Az enemy prefab
    private Transform player;                   // Játékos referenciája
    public Slider healthBarSlider;             // HP csúszka UI

    private bool enemiesSpawned = false;       // Az ellenségek már spawnoltak-e
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
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (GameManager.Instance.maxBossHealth != null)
        {
            maxHealth = GameManager.Instance.maxBossHealth;
        }
        UpdateHealthBar();
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
            Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * 5f; // Spawn pont a boss közelében
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
        if(currentHealth < GameManager.Instance.maxBossHealth / 2)
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
        // A boss halálának kezelése
        Instantiate(gatePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
