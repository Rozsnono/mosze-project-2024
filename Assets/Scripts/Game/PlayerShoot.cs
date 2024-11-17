using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // L�ved�k prefab
    private Transform firePoint;       // Kiindul�si pont a l�ved�k sz�m�ra
    private TextMeshProUGUI reloadTimerText;      // A UI Text elem, ahol az id�t megjelen�tj�k
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

        // Ellen�rz�s, hogy lehet-e �jra l�ni
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + PlayerStats.Instance.reloadTime)
        {
            Shoot();
            lastShotTime = Time.time;
        }

        // �jrat�lt�si id� sz�m�t�sa �s kijelz�se
        float timeSinceLastShot = Time.time - lastShotTime;
        float timeLeft = Mathf.Max(PlayerStats.Instance.reloadTime - timeSinceLastShot, 0);
        
        if(reloadTimerText != null)
        {
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
}
