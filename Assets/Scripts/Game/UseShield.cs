using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UseShield : MonoBehaviour
{
    public GameObject shieldPrefab;   // Pajzs prefab
    public Transform player;       // Játékos helye
    public bool shieldIsOn = false;

    private void Update()
    {
        // Ellenõrzés, hogy lehet-e újra lõni
        if (Input.GetMouseButtonDown(1) )
        {
            GameObject shield = Instantiate(shieldPrefab, player.position, Quaternion.identity);
            shieldIsOn = true;
        }

        if (player && shieldIsOn)
        {
            transform.position = player.position;
        }
    }
}
