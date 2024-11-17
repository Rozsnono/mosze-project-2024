using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float minSpeed = 0.5f;    // Minimális sebesség
    public float maxSpeed = 2f;      // Maximális sebesség
    private Vector2 movementDirection;
    private float speed;
    private int rotationSpeed;
    private Rigidbody2D rb;

    // Határok beállítása
    public float boundaryLimit = 100f; // Az a határ, ahol megsemmisül az aszteroida

    private void Start()
    {
        // Véletlenszerû sebesség beállítása a megadott minimum és maximum között
        speed = Random.Range(minSpeed, maxSpeed);
        rotationSpeed = Random.Range(-5, 5);

        rb = GetComponent<Rigidbody2D>();

        // Véletlenszerû irány beállítása
        getRandomDirection();
    }

    private void Update()
    {
        // Sebesség alkalmazása
        rb.velocity = movementDirection * speed;

        rb.rotation = rb.rotation + rotationSpeed;


        // Ellenõrzés, hogy az aszteroida túllépte-e a határt
        CheckBoundary();
    }

    private void CheckBoundary()
    {
        // Megsemmisítés, ha az aszteroida pozíciója túllépi a megadott határt
        if (Mathf.Abs(transform.position.x) > boundaryLimit || Mathf.Abs(transform.position.y) > boundaryLimit)
        {
            getRandomDirection();

        }
    }

    private void getRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

}
