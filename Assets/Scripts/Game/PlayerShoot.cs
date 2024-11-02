using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // L�ved�k prefab
    public GameObject homingPrefab;   // Rak�ta prefab
    public Transform firePoint;       // Kiindul�si pont a l�ved�k sz�m�ra
    public TextMeshProUGUI reloadTimerText;      // A UI Text elem, ahol az id�t megjelen�tj�k
    public float weaponIndex = 0;

    private float lastShotTime = 0f;
    private bool isReloading;

    private void Update()
    {
        // Ellen�rz�s, hogy lehet-e �jra l�ni
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + PlayerStats.Instance.reloadTime)
        {
            Shoot();
            lastShotTime = Time.time;
        }

        // �jrat�lt�si id� sz�m�t�sa �s kijelz�se
        float timeSinceLastShot = Time.time - lastShotTime;
        float timeLeft = Mathf.Max(PlayerStats.Instance.reloadTime - timeSinceLastShot, 0);

        // Friss�t�s a UI Text elemben
        if (timeLeft > 0)
        {
            reloadTimerText.text = $"{timeLeft:F1}s";
        }
        else
        {
            reloadTimerText.text = "0,0!";
        }
    }

    private void Shoot()
    {
        // L�ved�k l�trehoz�sa
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Az eg�r poz�ci�j�nak lek�r�se �s a l�ved�k ir�ny�nak be�ll�t�sa
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - firePoint.position).normalized;

        // A l�ved�k ir�ny�nak be�ll�t�sa
        bullet.GetComponent<Bullet>().SetDirection(shootDirection);
    }

    private void ShootHoming()
    {
        // Kattintott hely ellen�rz�se
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        // Ha tal�ltunk valamit, ami enemy vagy asteroid
        if (hit.collider != null)
        {
            GameObject targetObject = hit.collider.gameObject; // C�l objektum

            // Rak�ta l�trehoz�sa
            GameObject rocket = Instantiate(homingPrefab, firePoint.position, transform.rotation);
            HomingRocket homingRocket = rocket.GetComponent<HomingRocket>();
            
            Debug.Log(homingRocket);

            // Rak�ta c�lj�nak be�ll�t�sa
            if (homingRocket != null)
            {
                Debug.Log(true);
                homingRocket.SetTarget(targetObject.transform.position); // C�lpoz�ci� be�ll�t�sa
            }
        }
    }
}
