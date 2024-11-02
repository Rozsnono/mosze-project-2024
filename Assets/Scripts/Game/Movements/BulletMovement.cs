using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;

    private void Start()
    {
        // A lövedék 10 másodperc után megsemmisül
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        // Mozgatás a beállított irányba
        transform.Translate(direction * PlayerStats.Instance.bulletSpeed * Time.deltaTime, Space.World);
    }

    // Irány beállítása és forgatás az irányba
    public void SetDirection(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;

        // Forgatás beállítása a mozgás irányába
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        position.z = -1;
        transform.position = position;
    }
}
