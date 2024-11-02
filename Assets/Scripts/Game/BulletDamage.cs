using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDamage : MonoBehaviour
{
    public int damageAmount = 1; // Sebz�s m�rt�ke

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ellen�rzi, hogy az ellens�g rendelkezik-e `AdvancedEnemy` vagy b�rmilyen m�sik sebz�d� szkripttel
        var enemy = other.GetComponent<AdvancedEnemy>(); // Cser�lhet� m�sik sebz�d� objektumra is
        Debug.Log(enemy);
        if (enemy != null)
        {
            enemy.TakeDamage(5);  // Sebz�s alkalmaz�sa az ellens�gen
            Debug.Log("Damage");
            Destroy(gameObject);             // Rak�ta megsemmis�t�se �tk�z�s ut�n
        }
    }
}
