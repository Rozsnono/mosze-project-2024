using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
    public float moveSpeed = 2f;                // Az rak�ta sebess�ge

    private Transform player;                   // A j�t�kos poz�ci�j�nak t�rol�sa



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
            MoveTowardsPlayer();

            // Mindig n�zzen a j�t�kos fel�
            LookAtPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        // Mozg�s a j�t�kos ir�ny�ba
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        // Az ellens�g forgat�sa, hogy mindig a j�t�kos fel� n�zzen
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}