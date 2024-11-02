using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player; // Játékos pozíciója
    public float minX = -50f; // Bal oldali határ
    public float maxX = 50f;  // Jobb oldali határ
    public float minY = -50f; // Alsó határ
    public float maxY = 50f;  // Felsõ határ

    private void LateUpdate()
    {
        // A kamera pozíciója a játékos pozíciójához igazítva
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z; // A kamera Z pozíciója változatlan marad

        // Kamera pozíciójának korlátozása a megadott határok között
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}
