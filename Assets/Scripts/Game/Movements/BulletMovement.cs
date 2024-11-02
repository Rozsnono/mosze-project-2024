using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;

    private void Start()
    {
        // A l�ved�k 10 m�sodperc ut�n megsemmis�l
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        // Mozgat�s a be�ll�tott ir�nyba
        transform.Translate(direction * PlayerStats.Instance.bulletSpeed * Time.deltaTime, Space.World);
    }

    // Ir�ny be�ll�t�sa �s forgat�s az ir�nyba
    public void SetDirection(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;

        // Forgat�s be�ll�t�sa a mozg�s ir�ny�ba
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
