using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // Lovedek prefab
    private Transform firePoint;       // Kiindulasi pont a lovedek szamara
    private TextMeshProUGUI reloadTimerText;      // A UI Text elem, ahol az idot megjelenetjuk
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

        // Ellenorzes, hogy lehet-e ujra loni
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + PlayerStats.Instance.reloadTime)
        {
            Shoot();
            lastShotTime = Time.time;
        }

        // Ujratoltesi ido szamitasa es kijelzese
        float timeSinceLastShot = Time.time - lastShotTime;
        float timeLeft = Mathf.Max(PlayerStats.Instance.reloadTime - timeSinceLastShot, 0);
        
        if(reloadTimerText != null)
        {
            // Frissites a UI Text elemben
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
        // Lovedek letrehozasa
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Az eger poziciojanak lekerese es a lovedek iranyanak beellatasa
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - firePoint.position).normalized;

        // A lovedek iranyanak beallatasa
        bullet.GetComponent<Bullet>().SetDirection(shootDirection);
    }
}
