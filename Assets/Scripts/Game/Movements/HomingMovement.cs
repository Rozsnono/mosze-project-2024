using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    public float speed = 5f;               // Rakéta sebessége
    public float rotationSpeed = 200f;     // Rakéta forgási sebessége

    private Vector3 targetPosition;         // A rakéta célpozíciója

    private void Start()
    {
        // Inicializálás
        targetPosition = Vector3.zero; // Kezdetben nincsen cél
    }

    private void Update()
    {
        Debug.Log(targetPosition.x);
        Debug.Log(targetPosition.y);

        // Ha a rakétának van célja, akkor mozgás és irányítás
        if (targetPosition != Vector3.zero)
        {
            MoveTowardsTarget();
        }
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target; // Célpozíció beállítása
    }

    private void MoveTowardsTarget()
    {
        // Irány a cél felé
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Forgatás a cél irányába
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // Mozgás a cél felé
        transform.position += direction * speed * Time.deltaTime;

        // Opció: ha elérte a célját, eltávolítás
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject); // Eltávolítás a rakéta céljának elérése után
        }
    }
}
