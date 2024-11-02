using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float minSpeed = 0.5f;    // Minimális sebesség
    public float maxSpeed = 2f;      // Maximális sebesség
    private Vector2 movementDirection;
    private float speed;

    // Határok beállítása
    public float boundaryLimit = 20f; // Az a határ, ahol megsemmisül az aszteroida

    private void Start()
    {
        // Véletlenszerû sebesség beállítása a megadott minimum és maximum között
        speed = Random.Range(minSpeed, maxSpeed);

        // Véletlenszerû irány beállítása
        getRandomDirection();
    }

    private void Update()
    {
        // Mozgás a véletlenszerû irányba és sebességgel
        transform.Translate(movementDirection * speed * Time.deltaTime);

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
