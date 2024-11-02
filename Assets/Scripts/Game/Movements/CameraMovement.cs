using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // J�t�kos poz�ci�ja
    public float minX = -50f; // Bal oldali hat�r
    public float maxX = 50f;  // Jobb oldali hat�r
    public float minY = -50f; // Als� hat�r
    public float maxY = 50f;  // Fels� hat�r

    private void LateUpdate()
    {
        // A kamera poz�ci�ja a j�t�kos poz�ci�j�hoz igaz�tva
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z; // A kamera Z poz�ci�ja v�ltozatlan marad

        // Kamera poz�ci�j�nak korl�toz�sa a megadott hat�rok k�z�tt
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}
