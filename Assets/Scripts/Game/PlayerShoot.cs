using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // Lövedék prefab
    private Transform firePoint;       // Kiindulási pont a lövedék számára
    private TextMeshProUGUI reloadTimerText;      // A UI Text elem, ahol az idõt megjelenítjük
    public float weaponIndex = 0;

    private float lastShotTime = 0f;
    private bool isReloading;

    private void Start()
    {
        GameObject rTt = GameObject.FindGameObjectWithTag("ReloadText");
        reloadTimerText = rTt.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        firePoint = GameObject.FindGameObjectWithTag("Player").transform;

        // Ellenõrzés, hogy lehet-e újra lõni
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + PlayerStats.Instance.reloadTime)
        {
            Shoot();
            lastShotTime = Time.time;
        }

        // Újratöltési idõ számítása és kijelzése
        float timeSinceLastShot = Time.time - lastShotTime;
        float timeLeft = Mathf.Max(PlayerStats.Instance.reloadTime - timeSinceLastShot, 0);
        
        if(reloadTimerText != null)
        {
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
}
