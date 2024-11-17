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

        // L�trehozza a robban�s anim�ci�t
        // Robban�s prefab l�trehoz�sa
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(size, size, size);

        // Robban�s elt�vol�t�sa az anim�ci� lej�tsz�sa ut�n
        Destroy(explosion, time); // 1 m�sodperc (vagy az anim�ci� id�tartama)
    }
}
