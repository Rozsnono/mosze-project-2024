using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player; // J�t�kos poz�ci�ja
    public float width = 130f; // Oldal hat�r
    public float height = 140f; // Oldal hat�r

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        if (player != null) 
        { 
            // A kamera poz�ci�ja a j�t�kos poz�ci�j�hoz igaz�tva
            Vector3 newPosition = player.position;
            newPosition.z = transform.position.z; // A kamera Z poz�ci�ja v�ltozatlan marad

            // Kamera poz�ci�j�nak korl�toz�sa a megadott hat�rok k�z�tt
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
