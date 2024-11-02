using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // Lövedék prefab
    public GameObject homingPrefab;   // Rakéta prefab
    public Transform firePoint;       // Kiindulási pont a lövedék számára
    public TextMeshProUGUI reloadTimerText;      // A UI Text elem, ahol az idõt megjelenítjük
    public float weaponIndex = 0;

    private float lastShotTime = 0f;
    private bool isReloading;

    private void Update()
    {
        // Ellenõrzés, hogy lehet-e újra lõni
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + PlayerStats.Instance.reloadTime)
        {
            Shoot();
            lastShotTime = Time.time;
        }

        // Újratöltési idõ számítása és kijelzése
        float timeSinceLastShot = Time.time - lastShotTime;
        float timeLeft = Mathf.Max(PlayerStats.Instance.reloadTime - timeSinceLastShot, 0);

        // Frissítés a UI Text elemben
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
        // Lövedék létrehozása
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Az egér pozíciójának lekérése és a lövedék irányának beállítása
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - firePoint.position).normalized;

        // A lövedék irányának beállítása
        bullet.GetComponent<Bullet>().SetDirection(shootDirection);
    }

    private void ShootHoming()
    {
        // Kattintott hely ellenõrzése
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        // Ha találtunk valamit, ami enemy vagy asteroid
        if (hit.collider != null)
        {
            GameObject targetObject = hit.collider.gameObject; // Cél objektum

            // Rakéta létrehozása
            GameObject rocket = Instantiate(homingPrefab, firePoint.position, transform.rotation);
            HomingRocket homingRocket = rocket.GetComponent<HomingRocket>();
            
            Debug.Log(homingRocket);

            // Rakéta céljának beállítása
            if (homingRocket != null)
            {
                Debug.Log(true);
                homingRocket.SetTarget(targetObject.transform.position); // Célpozíció beállítása
            }
        }
    }
}
