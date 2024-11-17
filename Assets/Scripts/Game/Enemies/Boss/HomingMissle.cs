using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissle : MonoBehaviour
{
    public float moveSpeed = 2f;                // Az rakéta sebessége

    private Transform player;                   // A játékos pozíciójának tárolása



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
            MoveTowardsPlayer();

            // Mindig nézzen a játékos felé
            LookAtPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        // Mozgás a játékos irányába
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    private void LookAtPlayer()
    {
        // Az ellenség forgatása, hogy mindig a játékos felé nézzen
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}