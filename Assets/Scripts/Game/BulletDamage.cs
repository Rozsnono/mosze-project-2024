using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDamage : MonoBehaviour
{
    public int damageAmount = 1; // Sebzés mértéke

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ellenõrzi, hogy az ellenség rendelkezik-e `AdvancedEnemy` vagy bármilyen másik sebzõdõ szkripttel
        var enemy = other.GetComponent<AdvancedEnemy>(); // Cserélhetõ másik sebzõdõ objektumra is
        Debug.Log(enemy);
        if (enemy != null)
        {
            enemy.TakeDamage(5);  // Sebzés alkalmazása az ellenségen
            Debug.Log("Damage");
            Destroy(gameObject);             // Rakéta megsemmisítése ütközés után
        }
    }
}
