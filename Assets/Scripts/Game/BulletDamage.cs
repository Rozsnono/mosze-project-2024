using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDamage : MonoBehaviour
{
    public int damageAmount = 1; // Sebzes mértéke

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ellenorzi, hogy az ellenség rendelkezik-e `AdvancedEnemy` vagy bármilyen másik sebzés szkripttel
        var enemy = other.GetComponent<AdvancedEnemy>();
        Debug.Log(enemy);
        if (enemy != null)
        {
            enemy.TakeDamage(5);  // Sebzés alkalmazása az ellenségen
            Debug.Log("Damage");
            Destroy(gameObject);  // Rakéta megsemmisítése ütközés után
        }
    }
}
