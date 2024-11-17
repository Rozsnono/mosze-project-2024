using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float time = 0.4f;
    public float size;

    private void OnDestroy()
    {

        // Létrehozza a robbanás animációt
        // Robbanás prefab létrehozása
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(size, size, size);

        // Robbanás eltávolítása az animáció lejátszása után
        Destroy(explosion, time); // 1 másodperc (vagy az animáció idõtartama)
    }
}
