using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrustForce = 5f;        // Gyorsítás mértéke
    public float decelerationRate = 2f;   // Lassulási sebesség

    private Camera mainCamera;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Irányítás az egér irányába
        RotateTowardsMouse();

        // Elõre (W) és hátra (S) gyorsítás
        HandleMovement();

    }

    private void RotateTowardsMouse()
    {
        // Az egér pozíciójának lekérése a világban
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Forgatás az egér irányába
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            UpdateAnimation(true);
            // Elõre gyorsítás W gombbal
            rb.AddForce(transform.up * thrustForce);
            // Sebesség korlátozása
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, PlayerStats.Instance.speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Hátrafelé gyorsítás S gombbal
            rb.AddForce(-transform.up * thrustForce);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, PlayerStats.Instance.speed);
            UpdateAnimation(true);
        }
        else
        {
            // Lassulás, amikor nincs nyomva a W vagy S gomb
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, decelerationRate * Time.deltaTime);
            UpdateAnimation(false);

        }

        // Kamera határainak kiszámítása
        LimitMovementToCamera();
    }

    private void LimitMovementToCamera()
    {
        // Kamera széleinek meghatározása
        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;

        // Bal, jobb, felsõ, és alsó határok
        float minX = mainCamera.transform.position.x - halfWidth;
        float maxX = mainCamera.transform.position.x + halfWidth;
        float minY = mainCamera.transform.position.y - halfHeight;
        float maxY = mainCamera.transform.position.y + halfHeight;

        // Játékos pozíciójának korlátozása
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }

    private void UpdateAnimation(bool isMoving)
    {
        // Ellenõrizd, hogy mozog-e a hajó
        if (isMoving)
        {
            animator.Play("Moving");
        }
        else
        {
            animator.Play("Idle");
        }
    }
}
