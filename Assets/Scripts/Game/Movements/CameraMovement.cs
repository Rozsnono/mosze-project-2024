using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player; // Játékos pozíciója
    public float width = 130f; // Oldal határ
    public float height = 140f; // Oldal határ

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        if (player != null) 
        { 
            // A kamera pozíciója a játékos pozíciójához igazítva
            Vector3 newPosition = player.position;
            newPosition.z = transform.position.z; // A kamera Z pozíciója változatlan marad

            // Kamera pozíciójának korlátozása a megadott határok között
            newPosition.x = Mathf.Clamp(newPosition.x, -width, width);
            newPosition.y = Mathf.Clamp(newPosition.y, -height, height);

            transform.position = newPosition;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }
}
