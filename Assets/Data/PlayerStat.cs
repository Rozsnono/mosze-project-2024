using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public float bulletSpeed = 20f;
    public float maxHealth = 100f;
    public float maxShieldHealth = 50f;
    public float reloadTime = 5f;
    public float speed = 10f;
    public int skill = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpgradeBulletSpeed(float amount)
    {
        bulletSpeed += amount;
    }

    public void UpgradeMaxHealth(float amount)
    {
        maxHealth += amount;
    }

    public void UpgradeMaxShieldHealth(float amount)
    {
        maxShieldHealth += amount;
    }

    public void UpgradeSpeed(float amount)
    {
        speed += amount;
    }

    public void DecreaseReloadTime(float amount)
    {
        reloadTime = Mathf.Max(1f, reloadTime - amount); // minimum 1 másodperc
    }

    public void AddSkillPoint(int amount)
    {
        skill += amount;
    }
}
