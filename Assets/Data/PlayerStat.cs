using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public float bulletSpeed = 20f; //lövedék sebessége
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public float maxShieldHealth = 50f;
    public float reloadTime = 5f;
    public float shieldReloadTime = 15f;
    public float speed = 10f;
    public int skill = 0;

    public int playerShipIndex = 0;

    public float playerHealth = 100f;
    public float shieldHealth = 0;

    public int currectGame = 1;

    private void Awake()
    {
        if (Instance == null) // Biztosítja, hogy csak egy példány létezzen a PlayerStats-ból
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Megakadályozza a duplikációkat
        }
    }

    public void UpgradeBulletSpeed(float amount) // Lövedék sebességének növelése
    {
        bulletSpeed += amount;
    }

    public void UpgradeMaxHealth(float amount) // Maximális életerő növelése
    {
        maxHealth += amount;
    }

    public void Heal(float amount) // A játékos gyógyítása meghatározott értékkel
    {
        if(playerHealth + amount < maxHealth)
        {
            playerHealth += amount;
        }
    }

    public void UpgradeMaxShieldHealth(float amount) // Maximális pajzs életerő növelése
    {
        maxShieldHealth += amount;
    }

    public void HealShield(float amount) // Pajzs gyógyítása meghatározott értékkel
    {
        if (shieldHealth + amount < maxShieldHealth)
        {
            shieldHealth += amount;
        }
    }

    public void UpgradeSpeed(float amount) // Játékos mozgási sebességének növelése
    {
        speed += amount;
    }

    public void DecreaseReloadTime(float amount) // Újratöltési idő csökkentése, minimum 1 másodpercre
    {
        reloadTime = Mathf.Max(1f, reloadTime - amount);
    }

    public void AddSkillPoint(int amount) // Képességpontok hozzáadása a játékoshoz
    {
        skill += amount;
    }

    public void ChangePlayerShip(int amount) // Játékos által kiválasztott űrhajó módosítása
    {
        playerShipIndex = amount;
    }
}
