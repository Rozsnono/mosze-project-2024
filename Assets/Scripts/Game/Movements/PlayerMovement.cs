using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrustForce = 5f;        // Gyors�t�s m�rt�ke
    public float decelerationRate = 2f;   // Lassul�si sebess�g

    private Camera mainCamera;
    private Rigidbody2D rb;

    private void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Ir�ny�t�s az eg�r ir�ny�ba
        RotateTowardsMouse();

        // El�re (W) �s h�tra (S) gyors�t�s
        HandleMovement();
    }

    private void RotateTowardsMouse()
    {
        // Az eg�r poz�ci�j�nak lek�r�se a vil�gban
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Forgat�s az eg�r ir�ny�ba
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // El�re gyors�t�s W gombbal
            rb.AddForce(transform.up * thrustForce);
            // Sebess�g korl�toz�sa
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, PlayerStats.Instance.speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // H�trafel� gyors�t�s S gombbal
            rb.AddForce(-transform.up * thrustForce);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, PlayerStats.Instance.speed);
        }
        else
        {
            // Lassul�s, amikor nincs nyomva a W vagy S gomb
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, decelerationRate * Time.deltaTime);
        }

        // Kamera hat�rainak kisz�m�t�sa
        LimitMovementToCamera();
    }

    private void LimitMovementToCamera()
    {
        // Kamera sz�leinek meghat�roz�sa
        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;

        // Bal, jobb, fels�, �s als� hat�rok
        float minX = mainCamera.transform.position.x - halfWidth;
        float maxX = mainCamera.transform.position.x + halfWidth;
        float minY = mainCamera.transform.position.y - halfHeight;
        float maxY = mainCamera.transform.position.y + halfHeight;

        // J�t�kos poz�ci�j�nak korl�toz�sa
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }
}
