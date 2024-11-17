using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float minSpeed = 0.5f;    // Minim�lis sebess�g
    public float maxSpeed = 2f;      // Maxim�lis sebess�g
    private Vector2 movementDirection;
    private float speed;
    private int rotationSpeed;
    private Rigidbody2D rb;

    // Hat�rok be�ll�t�sa
    public float boundaryLimit = 100f; // Az a hat�r, ahol megsemmis�l az aszteroida

    private void Start()
    {
        // V�letlenszer� sebess�g be�ll�t�sa a megadott minimum �s maximum k�z�tt
        speed = Random.Range(minSpeed, maxSpeed);
        rotationSpeed = Random.Range(-5, 5);

        rb = GetComponent<Rigidbody2D>();

        // V�letlenszer� ir�ny be�ll�t�sa
        getRandomDirection();
    }

    private void Update()
    {
        // Sebess�g alkalmaz�sa
        rb.velocity = movementDirection * speed;

        rb.rotation = rb.rotation + rotationSpeed;


        // Ellen�rz�s, hogy az aszteroida t�ll�pte-e a hat�rt
        CheckBoundary();
    }

    private void CheckBoundary()
    {
        // Megsemmis�t�s, ha az aszteroida poz�ci�ja t�ll�pi a megadott hat�rt
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
