using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    public float speed = 5f;               // Rak�ta sebess�ge
    public float rotationSpeed = 200f;     // Rak�ta forg�si sebess�ge

    private Vector3 targetPosition;         // A rak�ta c�lpoz�ci�ja

    private void Start()
    {
        // Inicializ�l�s
        targetPosition = Vector3.zero; // Kezdetben nincsen c�l
    }

    private void Update()
    {
        Debug.Log(targetPosition.x);
        Debug.Log(targetPosition.y);

        // Ha a rak�t�nak van c�lja, akkor mozg�s �s ir�ny�t�s
        if (targetPosition != Vector3.zero)
        {
            MoveTowardsTarget();
        }
    }

    public void SetTarget(Vector3 target)
    {
        targetPosition = target; // C�lpoz�ci� be�ll�t�sa
    }

    private void MoveTowardsTarget()
    {
        // Ir�ny a c�l fel�
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Forgat�s a c�l ir�ny�ba
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // Mozg�s a c�l fel�
        transform.position += direction * speed * Time.deltaTime;

        // Opci�: ha el�rte a c�lj�t, elt�vol�t�s
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject); // Elt�vol�t�s a rak�ta c�lj�nak el�r�se ut�n
        }
    }
}
